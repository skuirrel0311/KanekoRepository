using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class Sprite : GameObject2D
    {
        public Vector2 origin;
        protected SpriteBatch spriteBatch;
        public Asset<Texture2D> texture { get; private set; }
        public float layer = 1.0f;

        public Sprite(string name, GameData gameData, Transform2D transform = null)
            : base(name, gameData, transform)
        {
        }

        public override void Load()
        {
            spriteBatch = gameData.spriteBatch;
            texture = gameData.spriteManager.GetContent(name);
        }

        public override void Draw()
        {
            if (texture.data == null) return;
            spriteBatch.Draw(texture.data, transform.position, null, Color.White, transform.rotation, origin, transform.scale, SpriteEffects.None, layer); 
        }

        public override void UnLoad()
        {
            texture.UnLoad();
        }
    }
}
