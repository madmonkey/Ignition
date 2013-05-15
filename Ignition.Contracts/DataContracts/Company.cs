

namespace Ignition.Contracts
{
    using ServiceStack.ServiceHost;
    using System;

    [Api("GET or DELETE a single entity by Id. Use POST to create a new and PUT to update it")]
    [Route("/customers", "POST,PUT,PATCH,DELETE")]
    [Route("/customers/{Id}")]
    public class Company : Request
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CompanyResponse
    {
        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        public Company Company { get; set; }
    }
    
}
