using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public interface IGlobalConfigurationRepository
    {
        public Task<GlobalConfiguration?> Search(MfeId name);
        public Task Save(GlobalConfiguration configuration);
        Task Update(GlobalConfiguration configuration);
    }
}
