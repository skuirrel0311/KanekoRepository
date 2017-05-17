using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class GameObject2D
    {
        public Transform2D transform;
        public string name;
        protected GameData gameData;

        public GameObject2D(string name, GameData gameData, Transform2D transform = null)
        {
            this.gameData = gameData;
            this.name = name;

            if (transform == null)
            {
                this.transform = new Transform2D();
            }
        }

        public virtual void Load() { }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw() { }

        public virtual void UnLoad() { }
    }
}
