using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using MfeClusterConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Infrastructure.Persistence
{
    public sealed class DynamoDbClusterConfigurationRepository : IMfeClusterConfigurationRepository
    {
        public DynamoDbClusterConfigurationRepository(IAmazonDynamoDB dynamoDb)
        {
            this.DynamoDb = dynamoDb;
        }

        public IAmazonDynamoDB DynamoDb { get; }

        public async Task Save(MfeClusterConfiguration mfeConfiguration)
        {
            var item = new Dictionary<string, AttributeValue>()
            {
                {"pk", new AttributeValue{ S= this.MfeIdFormatter(mfeConfiguration.MfeId.Value) } },
                {"sk", new AttributeValue{ S= this.ClusterIdFormatter(mfeConfiguration.ClusterId.Value) } },
                {"active", new AttributeValue{ S= mfeConfiguration.ActiveConfiguration.Value } },
                {"previous", new AttributeValue{ S= mfeConfiguration.Configurations[this.ConfigurationFormatter("previous")].Value } },
                {"current", new AttributeValue{ S=  mfeConfiguration.Configurations[this.ConfigurationFormatter("current")].Value } },
                {"preview", new AttributeValue{ S=  mfeConfiguration.Configurations[this.ConfigurationFormatter("preview")].Value } }
            };

            var request = new PutItemRequest()
            {
                TableName = "cxs-versioning",
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

        public async Task<MfeClusterConfiguration?> Search(MfeId name, ClusterId id)
        {
            var result = await this.GetSearchResult(name, id);
            if (result == null || result.Item == null)
            {
                return null;
            }
            result.Item.TryGetValue("active", out var activeConfiguration);
            result.Item.TryGetValue("previous", out var previous);
            result.Item.TryGetValue("current", out var current);
            result.Item.TryGetValue("preview", out var preview);
            var configurationList = new ConfigurationList(new Dictionary<string, string>() { { "previous", previous?.S ?? "" }, { "current", current?.S ?? "" }, { "preview", preview?.S ?? "" } });
            var configuration = MfeClusterConfiguration.Create(name, id, new ConfigurationList(configurationList), new MfeConfigurationName(activeConfiguration?.S ?? ""));
            return configuration;

            //Amazon.DynamoDBv2.AmazonDynamoDBClient
            //throw new NotImplementedException();
        }

        private async Task<GetItemResponse> GetSearchResult(MfeId name, ClusterId id)
        {
            var key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue(){ S = this.MfeIdFormatter(name.Value)}},
                { "sk", new AttributeValue(){ S = this.ClusterIdFormatter(id.Value)}}
            };
            var request = new GetItemRequest()
            {
                TableName = "cxs-versioning",
                Key = key
            };
            var result = await this.DynamoDb.GetItemAsync(request);
            return result;
        }

        private string MfeIdFormatter(string value) => $"a#{value}";
        private string ClusterIdFormatter(string value) => $"c#{value}";
        private MfeConfigurationName ConfigurationFormatter(string value) => new(value);
    }
}
