using System;
using System.Collections.Generic;

namespace SceneEditor.Core.Rendering
{
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// Resets the renderer area size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void ResetSize(int width, int height);

        /// <summary>
        /// Renders the specified scene snapshot to the correct rendering system
        /// </summary>
        /// <param name="snapshot"></param>
        void RenderScene(SceneSnapshot snapshot);
    }
}
