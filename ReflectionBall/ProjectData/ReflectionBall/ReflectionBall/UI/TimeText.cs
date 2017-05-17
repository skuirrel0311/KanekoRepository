using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class TimeText : Text
    {
        bool isCalc = false;

        float time = 0.0f;
        /// <summary>
        /// 表示する時間(秒)
        /// </summary>
        public float Time
        {
            get { return time; }
            set 
            {
                if (time == value) return;
                isCalc = true;
                time = value;
            }
        }

        public TimeText(GameData gameData, Vector2 position)
            :base(gameData, "TimeText", position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (!isCalc) return;
            isCalc = false;

            int minute = (int)time / 60;
            int second = (int)time - minute * 60;
            text = minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');
        }
    }
}
