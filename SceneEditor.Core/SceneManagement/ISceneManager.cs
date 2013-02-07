﻿using System.Collections.Generic;
using SceneEditor.Core.General;
using SceneEditor.Core.SceneManagement.Objects;

namespace SceneEditor.Core.SceneManagement
{
    public interface ISceneManager
    {
        Vector CameraPosition { get; }
        Vector CameraDimensions { get; }
        void MoveCameraBy(Vector moveBy);
        void MoveCameraTo(Vector finalPosition);
        void SetCameraDimensions(Vector dimensions);
        void Render();
        int AddBasicSceneSprite(int assetId, Vector position, Vector size);
        IEnumerable<ISceneObject> GetAllSceneObjects();
        ISceneObject GetObject(int id);
    }
}