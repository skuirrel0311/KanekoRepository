using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ReflectionBall
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GameData gameData = null;

        public Game1()
        {
            Content.RootDirectory = "Content";
            gameData = new GameData(this);
        }
        protected override void Initialize()
        {
            MyInputManager.MouseInitialize();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            gameData.Load();
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MyInputManager.MouseUpdate();
            if (MyInputManager.IsJustKeyDown(Keys.Escape)) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(gameData.backGroundColor);

            base.Draw(gameTime);
        }
    }
}
