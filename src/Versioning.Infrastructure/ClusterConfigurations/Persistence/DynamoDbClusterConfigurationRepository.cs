using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Versioning.Domain.ClusterConfigurations;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Infrastructure.ClusterConfigurations.Persistence
{
    public sealed class DynamoDbClusterConfigurationRepository : IClusterConfigurationRepository
    {
        private const string TableName = "cxs-version-configurations";
        public DynamoDbClusterConfigurationRepository(IAmazonDynamoDB dynamoDb)
        {
            this.DynamoDb = dynamoDb;
        }

        public IAmazonDynamoDB DynamoDb { get; }

        public async Task Save(ClusterConfiguration configuration)
        {
            var item = new Dictionary<string, AttributeValue>()
            {
                {"pk", new AttributeValue{ S= this.ClusterIdFormatter(configuration.ClusterId.Value) } },
                {"sk", new AttributeValue{ S= this.MfeIdFormatter(configuration.MfeId.Value) } },
                {"active", new AttributeValue{ S= configuration.ActiveConfiguration.Value } },
                {"previous", new AttributeValue{ S= configuration.Configurations[this.ConfigurationFormatter("previous")].Value } },
                {"current", new AttributeValue{ S=  configuration.Configurations[this.ConfigurationFormatter("current")].Value } },
                {"preview", new AttributeValue{ S=  configuration.Configurations[this.ConfigurationFormatter("preview")].Value } }
            };

            var request = new PutItemRequest()
            {
                TableName = TableName,
                Item = item
            };
            var response = await this.DynamoDb.PutItemAsync(request);
            if (response == null)
            {
                throw new SystemException("No cluster configuration found");
            }
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new SystemException("There are issues to retrieve the cluster configuration");
            }
        }

        public async Task<ClusterConfiguration?> Search(MfeId name, ClusterId id)
        {
            var result = await this.GetSearchResult(name, id);
            if (result == null || result.Item == null || result.HttpStatusCode == System.Net.HttpStatusCode.OK & result.Item.Count == 0)
            {
                return null;
            }
            var configuration = this.MapToConfiguration(name, id, result.Item);
            return configuration;
        }

        private ClusterConfiguration MapToConfiguration(MfeId name, Dictionary<string, AttributeValue> item)
        {
            item.TryGetValue("pk", out var clustedId);
            return this.MapToConfiguration(name, new ClusterId(this.ClusterIdUnFormatter(clustedId?.S ?? "")), item);
        }

        private ClusterConfiguration MapToConfiguration(MfeId name, ClusterId id, Dictionary<string, AttributeValue> item)
        {
            item.TryGetValue("active", out var activeConfiguration);
            item.TryGetValue("previous", out var previous);
            item.TryGetValue("current", out var current);
            item.TryGetValue("preview", out var preview);
            var configurationList = new ConfigurationList(new Dictionary<string, string>() { { "previous", previous?.S ?? "" }, { "current", current?.S ?? "" }, { "preview", preview?.S ?? "" } });
            var configuration = ClusterConfiguration.Create(name, id, new ConfigurationList(configurationList), new ConfigurationName(activeConfiguration?.S ?? ""));
            return configuration;
        }

        private async Task<GetItemResponse> GetSearchResult(MfeId name, ClusterId id)
        {
            var key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue(){ S = this.ClusterIdFormatter(id.Value)}},
                { "sk", new AttributeValue(){ S = this.MfeIdFormatter(name.Value)}},
            };
            var request = new GetItemRequest()
            {
                TableName = TableName,
                Key = key
            };
            var result = await this.DynamoDb.GetItemAsync(request);
            return result;
        }

        private string MfeIdFormatter(string value) => $"a#{value}";
        private string ClusterIdFormatter(string value) => $"c#{value}";
        private string ClusterIdUnFormatter(string value) => value[2..];
        private ConfigurationName ConfigurationFormatter(string value) => new(value);

        public async Task<List<ClusterConfiguration>> SearchBatch(MfeId name, List<ClusterId> clusterIds)
        {
            var keys = from c in clusterIds
                       select new Dictionary<string, AttributeValue>()
                       {
                           {"pk", new AttributeValue{ S = this.ClusterIdFormatter(c.Value)}},
                           {"sk", new AttributeValue{ S = this.MfeIdFormatter(name.Value)}},
                       };

            var versioningItems = new KeysAndAttributes
            {
                Keys = keys.ToList()
            };
            var requestItems = new Dictionary<string, KeysAndAttributes>
            {
                [TableName] = versioningItems
            };
            // Construct request
            var request = new BatchGetItemRequest
            {
                RequestItems = requestItems
            };

            BatchGetItemResponse result;
            var configurations = new List<ClusterConfiguration>();
            do
            {
                // Issue request and retrieve items
                result = await this.DynamoDb.BatchGetItemAsync(request);

                // Iterate through responses
                var responses = result.Responses;
                foreach (var tableName in responses.Keys)
                {
                    // Get items for each table
                    var tableItems = responses[tableName];

                    // View items
                    foreach (var item in tableItems)
                    {

                        configurations.Add(this.MapToConfiguration(name, item));

                    }
                }

                // Some items may not have been retrieved!
                //  Set RequestItems to the result's UnprocessedKeys and reissue request
                request.RequestItems = result.UnprocessedKeys;

            } while (result.UnprocessedKeys.Count > 0);

            return configurations;

            //throw new NotImplementedException();
        }

        public async Task UpdateBatch(List<ClusterConfiguration> configurations)
        {
            // THIS IS BAD! Please replace with BatchUpdate
            foreach (var configuration in configurations)
            {
                await this.Save(configuration);
            }
        }
    }
}
