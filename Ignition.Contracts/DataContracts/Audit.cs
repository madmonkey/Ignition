

namespace Ignition.Contracts
{
    using System;
    using ServiceStack.ServiceHost;

    [Api("GET or DELETE a single entity by Id. Use POST to create a new and PUT to update it")]
    [Route("/audit", "GET")]
    [Route("/audit/{pg}/{limit}","GET")]
    public class Audit : Pageable
    {
        
    }
    public class AuditResponse : Response
    {
        public int Id { get; set; }
        //public virtual string PrimaryKeyField { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
