using SceneEditor.Core.General;

namespace SceneEditor.Core.Commands.Objects
{
    public class CreateBasicSpriteCommand : ICommand
    {
        public int AssetId { get; set; }
        public Vector Position { get; set; }
        public bool PositionIsRelativeToCamera { get; set; }
    }
}
