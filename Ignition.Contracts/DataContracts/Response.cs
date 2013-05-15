namespace Ignition.Contracts
{
    using System;
    using System.Runtime.Serialization;

    public class Response : IExtensibleDataObject
    {
        public int Version { get; set; }
        public ExtensionDataObject ExtensionData
        {
            get; set;
            
        }
    }
}