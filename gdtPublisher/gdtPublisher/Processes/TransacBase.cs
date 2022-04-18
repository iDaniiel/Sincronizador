using System.Runtime.Serialization;

namespace gdtPublisher.Processes
{
    [DataContract]
    public abstract class TransacBase
    {
        public static bool Error { get; set; }

        [DataMember(Order = 1)]
        public string fcHash { get; set; }

        [DataMember(Order = 2)]
        public int piTipoOperacion { get; set; }
    }
}
