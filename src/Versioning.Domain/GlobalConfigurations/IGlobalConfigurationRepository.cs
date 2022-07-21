using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.GlobalConfigurations
{
    public interface IGlobalConfigurationRepository
    {
        public Task<GlobalConfiguration?> Search(MfeId name);
        public Task Save(GlobalConfiguration configuration);
        Task Update(GlobalConfiguration configuration);
    }
}
