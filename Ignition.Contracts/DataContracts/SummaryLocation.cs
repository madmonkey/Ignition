using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ignition.Contracts.DataContracts
{
    using ServiceStack.ServiceHost;

    [Api("GET summary by location")]
    [Route("/v1/locations")]
    public class SummaryLocation
    {
        
    }

    public class SummaryLocationResponse
    {
        public string Country { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
