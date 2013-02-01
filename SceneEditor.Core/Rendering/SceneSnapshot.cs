using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneEditor.Core.General;

namespace SceneEditor.Core.Rendering
{
    public class SceneSnapshot
    {
        public Vector CameraPosition { get; set; }
        public Vector RenderAreaDimensions { get; set; }
        public SceneSprite[] Sprites { get; set; }
    }
}
