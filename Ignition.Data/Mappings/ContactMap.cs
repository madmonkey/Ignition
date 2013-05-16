
namespace Ignition.Data.Mappings
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class ContactMap : ClassMap<ContactEntity>
    {
        public ContactMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            References(x => x.Company, "CompanyId").Cascade.All();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Title);
            Map(x => x.Category);
            HasMany(d => d.Addresses).KeyColumn("ContactId").Cascade.All();
            HasMany(d => d.ContactInformation).KeyColumn("ContactId").Cascade.All();
            Map(x => x.CreatedDateTime).Generated.Insert().Default("GETUTCDATE()");
            Map(x => x.LastUpdatedDateTime).Generated.Always().Default("GETUTCDATE()");
            Table("Contact");
        }
    }
}
