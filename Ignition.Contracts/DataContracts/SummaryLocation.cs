
namespace Ignition.Contracts
{
    using ServiceStack.ServiceHost;

    [Api("GET summary by location")]
    [Route("/locations", "GET")]
    public class SummaryLocation : Request { }

    public class SummaryLocationResponse : Response
    {
        public string Country { get; set; }
        public int Number { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
