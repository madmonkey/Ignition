using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ignition.Contracts
{
    using ServiceStack.ServiceHost;

    [Api("GET summary of categories")]
    [Route("/categories","GET")]
    public class SummaryCategory : Request { }
    public class SummaryCategoryResponse : Response
    {
        public string Category { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
