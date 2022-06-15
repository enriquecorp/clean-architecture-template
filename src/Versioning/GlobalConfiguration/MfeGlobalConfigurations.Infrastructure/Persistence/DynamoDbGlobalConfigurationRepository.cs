using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Infrastructure.Persistence
{
    public sealed class DynamoDbGlobalConfigurationRepository : IGlobalConfigurationRepository
    {
        private const string TableName = "cxs-version-configurations";
        public DynamoDbGlobalConfigurationRepository(IAmazonDynamoDB dynamoDb)
        {
            this.DynamoDb = dynamoDb;
        }

        public IAmazonDynamoDB DynamoDb { get; }
        public async Task Save(GlobalConfiguration configuration)
        {
            var item = new Dictionary<string, AttributeValue>()
            {
                {"pk", new AttributeValue{ S= this.MfeIdFormatter(configuration.MfeId.Value) } },
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
                throw new SystemException("No global configuration found");
            }
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new SystemException("There are issues to retrieve the global configuration");
            }
        }

        public async Task<GlobalConfiguration?> Search(MfeId name)
        {
            var result = await this.GetSearchResult(name);
            if (result == null || result.Item == null || (result.HttpStatusCode == System.Net.HttpStatusCode.OK & result.Item.Count == 0))
            {
                return null;
            }
            result.Item.TryGetValue("active", out var activeConfiguration);
            result.Item.TryGetValue("previous", out var previous);
            result.Item.TryGetValue("current", out var current);
            result.Item.TryGetValue("preview", out var preview);
            var configurationList = new ConfigurationList(new Dictionary<string, string>() { { "previous", previous?.S ?? "" }, { "current", current?.S ?? "" }, { "preview", preview?.S ?? "" } });
            var configuration = GlobalConfiguration.Create(name, new ConfigurationList(configurationList), new ConfigurationName(activeConfiguration?.S ?? ""));
            return configuration;
        }

        private async Task<GetItemResponse> GetSearchResult(MfeId name)
        {
            var key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue(){ S = this.MfeIdFormatter(name.Value)}},
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

        public async Task Update(GlobalConfiguration configuration)
        {
            await this.Save(configuration);
        }

        private string MfeIdFormatter(string value) => $"a#{value}";
        private ConfigurationName ConfigurationFormatter(string value) => new(value);
    }
}
