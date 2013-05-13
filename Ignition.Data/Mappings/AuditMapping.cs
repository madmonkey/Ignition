
namespace Ignition.Data.Mappings
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class AuditMapping : ClassMap<AuditEntity>
    {
        public AuditMapping()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("AuditId");
            Map(x => x.FieldName);
            Map(x => x.FieldType);
            Map(x => x.NewValue);
            Map(x => x.OldValue);
            Map(x => x.TableName);
            Map(x => x.Type);
            Map(x => x.PrimaryKeyField);
            Map(x => x.UpdateDate);
            Table("Audit");
        }
    }
}
