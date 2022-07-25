using Versioning.Domain.GlobalConfigurations;
using Versioning.Domain.Shared.Constants;
using Versioning.Domain.Shared.ValueObjects;
using Versioning.Shared.Tests.Domain.ValueObjects;

namespace Versioning.Domain.Tests.GlobalConfigurations
{
    public class GlobalConfigurationMother
    {
        public static GlobalConfiguration Create(MfeId name, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            return new GlobalConfiguration(name, activeConfiguration, configurations);
        }

        //public static ClusterConfiguration FromRequest(CreateClusterConfigurationCommand request)
        //{
        //    return Create(MfeIdMother.Create(request.MfeId), ClusterIdMother.Create(request.ClusterId), ConfigurationListMother.Create(request.Configurations), ConfigurationNameMother.Create(request.ActiveConfiguration));
        //}

        public static GlobalConfiguration Random()
        {
            var configurationName = ConfigurationNameMother.Random();
            var configurationListMinSize = Configuration.SupportedConfigurations.IndexOf(configurationName.Value) + 1;
            return Create(MfeIdMother.Random(), ConfigurationListMother.Random(configurationListMinSize), configurationName);
        }

        public static GlobalConfiguration? Empty()
        {
            return null;
        }
    }
}
