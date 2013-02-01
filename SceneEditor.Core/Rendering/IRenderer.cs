using System;
using System.Collections.Generic;

namespace SceneEditor.Core.Rendering
{
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// Renders the specified scene snapshot to the correct rendering system
        /// </summary>
        /// <param name="snapshot"></param>
        void RenderScene(SceneSnapshot snapshot);
    }
}
