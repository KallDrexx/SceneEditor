using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SceneEditor.Core.Rendering
{
    public class SceneSnapshot
    {
        public Vector2 CameraPosition { get; set; }
        public Vector2 RenderAreaDimensions { get; set; }
        public SceneSprite[] Sprites { get; set; }
    }
}
