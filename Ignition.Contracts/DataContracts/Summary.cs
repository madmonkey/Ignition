
namespace Ignition.Contracts
{
    using System.Collections.Generic;
    using ServiceStack.ServiceHost;

    [Api("GET summary of categories")]
    [Route("/summaries","GET")]
    public class Summary : Request { }

    public class SummaryResponse : Response
    {
        public List<SummaryLocationResponse> ByLocation { get; set; }
        public List<SummaryCategoryResponse> ByCategory { get; set; }
    }
    public class SummaryLocationResponse : Response
    {
        public string Country { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
    public class SummaryCategoryResponse : Response
    {
        public string Category { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
