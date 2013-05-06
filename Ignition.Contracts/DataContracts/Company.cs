

namespace Ignition.Contracts
{
    using ServiceStack.ServiceHost;
    using System;

    [Api("GET or DELETE a single entity by Id. Use POST to create a new and PUT to update it")]
    [Route("/v1/customers", "POST,PUT,PATCH,DELETE")]
    [Route("/v1/customers/{Id}")]
    public class Company
    {
        /// <summary>
        /// Initializes a new instance of the movie.
        /// </summary>
        public Company()
        {
            //this.Genres = new List<string>();
        }

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
