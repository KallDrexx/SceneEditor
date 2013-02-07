using SceneEditor.Core.General;

namespace SceneEditor.Core.Commands.Objects
{
    public class CreateBasicSpriteCommand : ICommand
    {
        public string Name { get { return "Create Sprite"; } }
        public int AssetId { get; set; }
        public Vector Position { get; set; }
        public bool PositionIsRelativeToCamera { get; set; }
    }
}
