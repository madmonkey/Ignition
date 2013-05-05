
namespace Ignition.Data.Mappings
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class AddressMapping : ClassMap<AddressEntity>
    {
        public AddressMapping()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            References(x => x.Contact).Column("ContactId").Cascade.All();
            Map(x => x.Address);
            Map(x => x.City);
            Map(x => x.Country);
            Map(x => x.PostalCode);
            Map(x => x.Region);
            Map(x => x.CreatedDateTime).Generated.Insert().Default("GETUTCDATE()");
            Map(x => x.LastUpdatedDateTime).Generated.Always().Default("GETUTCDATE()");
        }
    }
}
