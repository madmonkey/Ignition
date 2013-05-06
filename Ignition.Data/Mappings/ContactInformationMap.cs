
namespace Ignition.Data.Mappings
{
    using FluentNHibernate.Mapping;
    using Ignition.Data.Entities;

    public class ContactInformationMap : ClassMap<ContactInformationEntity>
    {
        public ContactInformationMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            References(x => x.Contact).Column("ContactId").Cascade.All();
            Map(x => x.ContactType);
            Map(x => x.ContactValue);
            Map(x => x.CreatedDateTime).Generated.Insert().Default("GETUTCDATE()");
            Map(x => x.LastUpdatedDateTime).Generated.Always().Default("GETUTCDATE()");
            Table("ContactInformation");
        }
    }
}
