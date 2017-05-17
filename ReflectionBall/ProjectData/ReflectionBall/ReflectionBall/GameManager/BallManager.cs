using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class BallManager : GameObject2D
    {
        public static BallManager I;


        Scene scene;
        public Stage stage;
        List<Ball> ballList = new List<Ball>();

        int maxBallNum;
        Random rand;

        public float ballSize = 0.25f;
        Texture2D ballTex;

        public SphereCollider2D mouseCollider;

        public BallManager(GameData gameData, Scene scene, Stage stage)
            :base("BallManager", gameData)
        {
            I = this;
            this.gameData = gameData;
            this.scene = scene;
            this.stage = stage;

            mouseCollider = new SphereCollider2D(Vector2.Zero, 0.1f);
            maxBallNum = GameData.MaxBallNum;

            rand = new Random();
        }

        public override void Load()
        {
            ballTex = gameData.spriteManager.GetContent("Sprite\\Ball").data;
        }

        /// <summary>
        /// 戻り値は生成できたか？
        /// </summary>
        public bool GenerateBall(float positionX, bool isRandom = false)
        {
            if (ballList.Count + 1 > maxBallNum)
            {
                return false;
            }

            Ball ball = new Ball(gameData, "Sprite\\Ball");
            if (isRandom)
            {
                int temp = (int)(ballTex.Width * ballSize);
                positionX = rand.Next(temp, GameData.windowWidth - temp);
            }
            ball.transform.position = new Vector2(positionX, 0.0f);
            ball.transform.scale = ballSize;

            ballList.Add(ball);
            scene.Instantiate(ball);

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            mouseCollider.center = MyInputManager.MousePosition;
        }

        public Ball Intersects(Ball ball)
        {
            foreach (Ball b in ballList)
            {
                if (b.Equals(ball)) continue;
                if (b.collider == null) continue;
                if (ball.collider.Intersects(b.collider)) return b;
            }

            return null;
        }

        public void Destroy(Ball ball)
        {
            ballList.Remove(ball);
            scene.Destroy(ball);
        }
    }
}
