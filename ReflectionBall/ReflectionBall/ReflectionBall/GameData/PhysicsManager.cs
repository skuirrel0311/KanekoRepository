using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public struct PhysicsManager
    {
        /// <summary>
        /// 空気抵抗
        /// </summary>
        public float drag { get; private set; }

        /// <summary>
        /// 重力
        /// </summary>
        public float gravity { get; private set; }

        /// <summary>
        /// 反発係数
        /// </summary>
        public float bounciness { get; private set; }

        /// <summary>
        /// マウスに当たった時の反発係数
        /// </summary>
        public float mouseBounciness { get; private set; }

        public void SetValue(float drag, float gravity, float bounciness, float mouseBounciness = 0.0f)
        {
            this.drag = drag;
            this.gravity = gravity;
            this.bounciness = bounciness;
            this.mouseBounciness = mouseBounciness;
        }

        public void SetBallPhysicsValue()
        {
            drag = 0.99f;
            gravity = 0.15f;
            bounciness = 8.0f;
            mouseBounciness = 12.0f;
        }
    }
}
