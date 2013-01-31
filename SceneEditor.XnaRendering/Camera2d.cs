using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SceneEditor.XnaRendering
{
    internal class Camera2D
    {
        private readonly SpriteBatch _spriteBatch;

        public Vector2 Position { get; set; }

        public Camera2D(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            Position = new Vector2(0, 0);
        }

        public void Draw(Scene2DNode node)
        {
            var drawPosition = ApplyTransformations(node.WorldPosition);
            node.Draw(_spriteBatch, drawPosition);
        }

        public void Move(Vector2 moveVector)
        {
            Position += moveVector;
        }

        private Vector2 ApplyTransformations(Vector2 nodePosition)
        {
            var finalPosition = nodePosition - Position;
            return finalPosition;
        }
    }
}
