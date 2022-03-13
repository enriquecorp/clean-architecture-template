﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class MfeGlobalConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }

        public MfeConfigurationName ActiveConfiguration { get; private set; }

        public VersionList Versions { get; private set; }

        public MfeGlobalConfiguration(MfeId name, MfeConfigurationName active, VersionList versions)
        {
            this.MfeId = name;
            this.ActiveConfiguration = active;
            this.Versions = versions;
        }

        public static MfeGlobalConfiguration Create(MfeId name, VersionList versions, MfeConfigurationName? active = null)
        {
            var configuration = new MfeGlobalConfiguration(name, active ?? GetFirstConfiguration(versions), versions);

            //configuration.Record(
            //new MfeConfigurationDomainEvent(
            //    id.Value, name.Value));

            return configuration;
        }

        /// <summary>
        /// This will merge the incoming version list with the current one
        /// </summary>
        /// <param name="versions"></param>
        public void UpdateVersions(VersionList versions)
        {
            this.Versions = versions;
        }

        private static MfeConfigurationName GetFirstConfiguration(VersionList versions) => versions.GetFirstConfigurationName();
    }
}
