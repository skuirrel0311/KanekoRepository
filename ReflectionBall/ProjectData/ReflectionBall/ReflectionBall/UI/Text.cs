using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class Text : GameObject2D
    {
        protected SpriteBatch spriteBatch;
        protected SpriteFont spriteFont;

        public string text;
        public Color color = Color.Black;
        Vector2 origin;
        protected float layer = 0.5f;
        public bool isDraw = true;

        public Text(GameData gameData, string text, Vector2 position)
            : base(text, gameData)
        {
            this.text = text;
            transform.position = position;
            transform.scale = 16.0f;
        }

        public override void Load()
        {
            this.spriteFont = gameData.spriteFont;
            this.spriteBatch = gameData.spriteBatch;

            transform.scale *= 0.01f;
            origin = spriteFont.MeasureString(text) * 0.5f;
        }

        public override void Draw()
        {
            if (!isDraw) return;
            spriteBatch.DrawString(spriteFont, text, transform.position, color, transform.rotation, origin, transform.scale, SpriteEffects.None, layer);
        }
    }
}
