using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class SphereCollider2D
    {
        public Vector2 center;
        public float radius;

        public SphereCollider2D(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool Intersects(SphereCollider2D collider)
        {
            Vector2 temp = center - collider.center;
            float r = collider.radius + radius;
            
            //３平方の定理
            return ((temp.X * temp.X) + (temp.Y * temp.Y)) < (r * r);
        }
    }
}
