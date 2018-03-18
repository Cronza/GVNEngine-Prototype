using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace VisualNovelEngine.EngineFiles.Collections
{
    /// <summary>
    /// Custom Container mimicking the structure needed for Sprite Batch
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle Size { get; set; }
        public Vector2 Pos { get; set; }
        public Color Color { get; set; }
    }

    /// <summary>
    /// Custom container used for easily drawing and updating a currently rendered video
    /// </summary>
    public class VideoRender
    {
        public Sprite Render;
        public VideoPlayer Player;
    }
}
