
namespace Ignition.Contracts
{
    /// <summary>
    /// The purpose of this class is to version the service contract.
    /// </summary>
    public static class Information
    {
        /// <summary>
        /// The root namespace for service.
        /// </summary>
        public const string NamespaceRoot = "http://www.ignition.com/service/";

        /// <summary>
        /// The purpose of this class is to version the service contract.
        /// </summary>
        public static class Namespace
        {
            /// <summary>
            /// The version constant for service.
            /// </summary>
            public const string Ignition = Information.NamespaceRoot + "Ignition/2013/05/";
        }
    }
}

