using System.Collections.Generic;

namespace GVNXMLData
{
    /// <summary>
    /// The data object for deserialized story data
    /// </summary>
    public class StoryChapter
    {
        public string ChapterName { get; set; }
        public List<List<string>> ChapterText { get; set; }
    }
}
