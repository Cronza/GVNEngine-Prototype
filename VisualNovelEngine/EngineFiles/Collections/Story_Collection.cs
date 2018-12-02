using System.Collections.Generic;

namespace VisualNovelEngine.EngineFiles.Collections
{
    /// <summary>
    /// The data object for deserialized JSON story data
    /// </summary>
    public class StoryChapter
    {
        public string ChapterName { get; set; }
        public List<List<string>> ChapterText { get; set; }
    }
}
