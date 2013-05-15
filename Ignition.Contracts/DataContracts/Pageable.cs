namespace Ignition.Contracts
{
    /// <summary>
    /// Provides consistent url for paging
    /// </summary>
    public class Pageable : Request
    {
        public int pg { get; set; }
        public int limit { get; set; }
    }
}