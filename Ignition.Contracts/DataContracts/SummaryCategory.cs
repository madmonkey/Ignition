using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ignition.Contracts.DataContracts
{
    using ServiceStack.ServiceHost;

    [Api("GET summary of categories")]
    [Route("/v1/categories")]
    public class SummaryCategory{}
    public class SummaryCategoryResponse
    {
        public string Category { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
