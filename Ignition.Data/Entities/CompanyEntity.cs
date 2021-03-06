﻿namespace Ignition.Data.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A company that a 'contact' is associated with
    /// </summary>
    public class CompanyEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual IList<ContactEntity> Contacts { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
    }
}
