using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class Transform2D
    {
        public Vector2 position;
        public float scale;
        public float rotation;

        public Transform2D()
        {
            position = Vector2.Zero;
            scale = 1.0f;
            rotation = 0.0f;
        }

        public Transform2D(Vector2 position, float size = 1.0f, float rotation = 0.0f)
        {
            this.position = position;
            this.scale = size;
            this.rotation = rotation;
        }
    }
}
