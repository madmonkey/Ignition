﻿
namespace Ignition.Data.Mappings
{
    using Entities;
    using FluentNHibernate.Mapping;
    
    /// <summary>
    /// The entity mapping
    /// </summary>
    public class CompanyMap : ClassMap<CompanyEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyMap"/> class.
        /// </summary>
        public CompanyMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name);
            Map(x => x.Code);
            HasMany(d => d.Contacts).KeyColumn("CompanyId").Cascade.All();
            Map(x => x.CreatedDateTime).Generated.Insert().Default("GETUTCDATE()");
            Map(x => x.LastUpdatedDateTime).Generated.Always().Default("GETUTCDATE()");
            Table("Company");
        }
    }
    
}
