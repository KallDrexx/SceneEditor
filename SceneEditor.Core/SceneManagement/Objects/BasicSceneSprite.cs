using SceneEditor.Core.General;

namespace SceneEditor.Core.SceneManagement.Objects
{
    public class BasicSceneSprite : ISceneObject
    {
        public int Id { get; set; }
        public Vector StartPosition { get; set; }
        public Vector Dimensions { get; set; }
        public string AssetName { get; set; }

        public override string ToString()
        {
            return "BasicSceneSprite: " + Id;
        }
    }
}
