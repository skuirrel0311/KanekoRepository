using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ReflectionBall
{
    public class GameData
    {
        public const int windowWidth = 1280;
        public const int windowHeight = 720;

        /// <summary>
        /// このゲームの制限時間
        /// </summary>
        public const float LimitTime = 20.0f;
        /// <summary>
        /// ボールが一度に存在できる最大の数
        /// </summary>
        public const int MaxBallNum = 3;

        public Game game;
        public Score score;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont spriteFont;
        public Color backGroundColor = Color.CornflowerBlue;

        public SceneManager sceneManager;
        public PhysicsManager ballPhysics;
        public NotificationManager notificationManager;

        public MYContentManager<Texture2D> spriteManager;
        public MYContentManager<SoundEffect> soundEffectManager;
        public MYContentManager<Song> songManager;

        public GameData(Game game)
        {
            this.game = game;
            score = new Score();

            graphics = new GraphicsDeviceManager(game);
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            game.IsMouseVisible = true;

            ballPhysics.SetBallPhysicsValue();
            sceneManager = new SceneManager(this);
            notificationManager = new NotificationManager(this);

            game.Components.Add(sceneManager);

            spriteManager = new MYContentManager<Texture2D>(game);
            soundEffectManager = new MYContentManager<SoundEffect>(game);
            songManager = new MYContentManager<Song>(game);
        }

        public void Load()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            spriteFont = game.Content.Load<SpriteFont>("SpriteFont1");
            sceneManager.ChangeScene("TitleScene");
        }
    }
}
