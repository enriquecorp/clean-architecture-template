﻿using MfeTenantConfigurations.Domain.Events;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeTenantConfigurations.Domain
{
    public sealed class MfeTenantConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }
        public TenantId TenantId { get; private set; }
        public MfeConfigurationName ActiveConfiguration { get; private set; }
        public ConfigurationList Configurations { get; private set; }

        public MfeTenantConfiguration(TenantId id, MfeId name, MfeConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            this.TenantId = id;
            this.MfeId = name;
            this.ActiveConfiguration = activeConfiguration;
            this.Configurations = configurations;
        }

        public static MfeTenantConfiguration Create(MfeId name, TenantId id, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            var configuration = new MfeTenantConfiguration(id, name, activeConfiguration, configurations);
            configuration.Record(new MfeTenantConfigurationCreatedDomainEvent($"{name.Value}#{id.Value}", configurations.Value, activeConfiguration.Value));

            return configuration;
        }

        public void UpdateConfigurations(ConfigurationList configurations)
        {
            // this.Versions = versions;
            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersionUrl);
                if (incomingVersionUrl != null && this.Configurations.ContainsKey(item.Key))
                {
                    this.Configurations[item.Key] = incomingVersionUrl; // it will update only if the incoming versionUrl has a value
                    this.Record(new VersionUrlChangedDomainEvent($"{this.MfeId.Value}#{this.TenantId.Value}", configurationName: item.Key.Value, versionUrl: incomingVersionUrl.Value));
                }
            }
        }

        public void UpdateActiveConfiguration(MfeConfigurationName configuration)
        {
            this.ActiveConfiguration = configuration;
            this.Record(new MfeActiveTenantConfigurationChangedDomainEvent($"{this.MfeId.Value}#{this.TenantId.Value}", this.ActiveConfiguration.Value));
        }
    }
}