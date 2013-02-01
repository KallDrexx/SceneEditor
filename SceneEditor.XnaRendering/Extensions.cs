using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SceneEditor.Core.General;

namespace SceneEditor.XnaRendering
{
    internal static class Extensions
    {
        public static Vector2 ToXnaVector(this Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}
