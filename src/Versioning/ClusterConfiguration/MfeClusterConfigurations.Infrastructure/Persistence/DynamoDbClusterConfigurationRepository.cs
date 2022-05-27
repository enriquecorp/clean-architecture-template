using MfeClusterConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Infrastructure.Persistence
{
    public sealed class DynamoDbClusterConfigurationRepository : IMfeClusterConfigurationRepository
    {
        public Task Save(MfeClusterConfiguration mfeConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task<MfeClusterConfiguration?> Search(MfeId name, ClusterId id)
        {
            //Amazon.DynamoDBv2.AmazonDynamoDBClient
            throw new NotImplementedException();
        }
    }
}
