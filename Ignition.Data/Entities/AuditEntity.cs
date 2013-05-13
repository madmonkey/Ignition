using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignition.Data.Entities
{
    public class AuditEntity
    {
        public virtual int Id { get; set; }
        public virtual string PrimaryKeyField { get; set; }
        public virtual string Type { get; set; }
        public virtual string TableName { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string FieldType { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual DateTime UpdateDate { get; set; }
    }
}
