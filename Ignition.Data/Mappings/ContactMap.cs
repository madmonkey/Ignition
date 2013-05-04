
namespace Ignition.Data.Mappings
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class ContactMap : ClassMap<ContactEntity>
    {
        public ContactMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            References<AddressEntity>(x => x.CompanyId).Column("CompanyId").Cascade.All();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Title);
            Table("Contact");
        }
    }
}
