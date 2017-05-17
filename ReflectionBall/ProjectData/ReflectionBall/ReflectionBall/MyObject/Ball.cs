using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class Ball : Sprite
    {
        BallManager manager;
        PhysicsManager ballPhysics;
        Score score;

        public SphereCollider2D collider { get; private set; }

        public float speed = 50.0f;
        //移動量
        Vector2 velocity;
        //慣性
        Vector2 innerVec;

        Timer accelTimer;
        Timer invincibleTimer;
        Color color = Color.White;

        MySoundEffect reflectionSE;

        public Ball(GameData gameData, string name, Transform2D transform = null)
            : base(name, gameData, transform)
        {
            this.manager = BallManager.I;
            score = gameData.score;

            reflectionSE = new MySoundEffect("Audio\\jump", gameData);
            //音の重なり防止
            invincibleTimer = new Timer(0.2f);
            //一定間隔ごとに加速する
            accelTimer = new Timer(6.0f);
            gameData.sceneManager.currentScene.Instantiate(invincibleTimer);
            gameData.sceneManager.currentScene.Instantiate(accelTimer);

            velocity = Vector2.Zero;
            //最初に0だと上の壁に阻まれる
            velocity.Y = 0.1f;
            innerVec = Vector2.Zero;

            this.ballPhysics = gameData.ballPhysics;
        }

        public override void Load()
        {
            reflectionSE.Load();
            base.Load();
        }

        public override void Initialize()
        {
            origin.X = texture.data.Width * 0.5f;
            origin.Y = texture.data.Height * 0.5f;

            float radius = 250.0f * transform.scale;
            collider = new SphereCollider2D(transform.position, radius);
            accelTimer.ReStart();
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePhysics();

            UpdateColor();

            if (accelTimer.isLimitTime)
            {
                speed *= 1.2f;
                accelTimer.ReStart();
            }

            innerVec.Y += ballPhysics.gravity;
            innerVec.X *= ballPhysics.drag;

            velocity = innerVec;
            Move(velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * speed);
        }

        void UpdatePhysics()
        {
            //マウスとの当たり判定
            if (collider.Intersects(manager.mouseCollider) && !invincibleTimer.isStart)
            {
                invincibleTimer.ReStart();
                reflectionSE.Play();
                MouseReflection(manager.mouseCollider.center);
            }

            //ボール同士の当たり判定
            Ball ball = manager.Intersects(this);

            if (ball != null)
            {
                Vector2 normal = Vector2.Normalize(transform.position - ball.transform.position);
                //法線と同じ方向を向いていない
                if (Vector2.Dot(velocity, normal) < 0.0f)
                {
                    velocity = Vector2.Reflect(velocity, normal);
                    Reflection(velocity);
                    ball.Reflection(-velocity);
                }
            }

            //場外判定
            Wall wall = manager.stage.Intersects(collider);
            if (wall == null) return;

            if (wall.direction == Wall.Direction.Down)
            {
                //下に落ちた場合は消滅
                manager.Destroy(this);
                return;
            }

            //法線と同じ方向を向いていたらおかしい
            if (Vector2.Dot(velocity, wall.normal) > 0.0f) return;

            //反射する
            velocity = Vector2.Reflect(velocity, wall.normal);
            Reflection(velocity);
        }

        void UpdateColor()
        {
            if (!invincibleTimer.isStart) return;

            if (invincibleTimer.progress < 0.5f)
                color = Color.Lerp(Color.White, Color.CornflowerBlue, invincibleTimer.progress * 2.0f);
            else
                color = Color.Lerp(Color.CornflowerBlue, Color.White, (invincibleTimer.progress - 0.5f) * 2.0f);
        }

        /// <summary>
        /// マウスに当たった時の反射
        /// </summary>
        public void MouseReflection(Vector2 mousePositon)
        {
            Vector2 vec = Vector2.Normalize(transform.position - mousePositon);

            if (transform.position.Y > mousePositon.Y)
            {
                innerVec = vec * -ballPhysics.mouseBounciness;
            }
            else
            {
                innerVec = vec * ballPhysics.mouseBounciness;
            }

            score.Reflected(this);
        }

        public void Reflection(Vector2 vec)
        {
            vec = Vector2.Normalize(vec);

            innerVec = vec * ballPhysics.bounciness;
        }

        void Move(Vector2 velocity)
        {
            transform.position += velocity;
            collider.center += velocity;
        }

        public override void Draw()
        {
            if (texture.data == null) return;
            spriteBatch.Draw(texture.data, transform.position, null, color, transform.rotation, origin, transform.scale, SpriteEffects.None, layer);
        }
    }
}
