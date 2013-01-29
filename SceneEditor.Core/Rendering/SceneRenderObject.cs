using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Core.Rendering
{
    public class SceneRenderObject
    {
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Alpha { get; set; }
    }
}
