using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SceneEditor.XnaRendering
{
    internal class Scene2DNode
    {
        private readonly Texture2D _texture;

        public Vector2 WorldPosition { get; set; }

        public Scene2DNode(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            WorldPosition = position;
        }

        public void Draw(SpriteBatch renderer, Vector2 drawPosition)
        {
            renderer.Draw(_texture, drawPosition, Color.White);
        }
    }
}
