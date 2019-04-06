using System.Collections.Generic;

namespace GVNXMLData
{
    /// <summary>
    /// The data object for deserialized engine settings
    /// </summary>
    public class EngineProperties
    {
        public List<int> Resolution { get; set; }
        public bool EnableUserResizing { get; set; }
    }
}
