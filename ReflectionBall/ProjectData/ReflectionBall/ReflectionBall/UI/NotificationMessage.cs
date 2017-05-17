using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class NotificationMessage : Text
    {
        enum State { 
            Came, //出現してくる
            View, //見えている
            GoOut //消えていく
        }

        State state;

        Timer timer;
        float[] times = new float[3];
        
        public Color targetColor = Color.Gray;
        Color startColor = Color.Transparent;

        public NotificationMessage(GameData gameData, string message,Vector2 position, float cameTime, float viewTime, float outTime)
            :base(gameData, message, position)
        {
            color = startColor;
            layer = 0.0f;

            state = State.Came;
            times[(int)State.Came] = cameTime;
            times[(int)State.View] = viewTime;
            times[(int)State.GoOut] = outTime;

            timer = new Timer();
            timer.Start(times[(int)state]);
            gameData.sceneManager.currentScene.Instantiate(timer);
        }

        public override void Update(GameTime gameTime)
        {
            if (timer.isLimitTime)
            {
                //次に行く
                state = (State)state + 1;
                if ((int)state >= times.Length)
                {
                    //範囲外に行ったら終了
                    gameData.sceneManager.currentScene.Destroy(this);
                    return;
                }
                timer.Start(times[(int)state]);
            }

            if (!timer.isStart) return;

            UpdateColor();
        }

        void UpdateColor()
        {
            switch (state)
            {
                case State.Came:
                    color = Color.Lerp(startColor, targetColor, timer.progress * timer.progress);
                    break;
                case State.GoOut:
                    color = Color.Lerp(targetColor, startColor, timer.progress);
                    break;
            }
        }

        public override void Draw()
        {
            if (!timer.isStart) return;
            base.Draw();
        }
    }
}
