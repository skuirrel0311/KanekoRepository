using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class Timer : GameObject2D
    {
        public float currentTime { get; private set; }
        public float limitTime { get; private set; }
        public bool isStart { get; private set; }
        public bool isLimitTime { get; private set; }
        public float progress { get { return currentTime / limitTime; } }

        public Timer(float limitTime = 0.0f)
            :base("Timer", null)
        {
            this.limitTime = limitTime;
            isStart = false;
        }

        public void ReStart()
        {
            currentTime = 0.0f;
            isLimitTime = false;
            isStart = true;
        }

        public void Start(float limitTime)
        {
            this.limitTime = limitTime;
            ReStart();
        }

        public void Stop()
        {
            currentTime = 0.0f;
            isLimitTime = false;
            isStart = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isStart) return;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime > limitTime)
            {
                isLimitTime = true;
                isStart = false;
            }
        }
    }
}
