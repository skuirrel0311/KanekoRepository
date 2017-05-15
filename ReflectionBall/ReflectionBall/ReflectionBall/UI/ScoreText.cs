using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    class ScoreText : Text
    {
        bool isCalc = true;

        int scoreVale;
        /// <summary>
        /// 表示する時間(秒)
        /// </summary>
        public int ScoreValue
        {
            get { return scoreVale; }
            set
            {
                if (value == scoreVale) return;
                isCalc = true;
                scoreVale = value;
            }
        }

        Score score;

        Text title;

        public ScoreText(GameData gameData, Vector2 position, Score score)
            : base(gameData, "ScoreText", position)
        {
            this.score = score;
        }

        public override void Initialize()
        {
            title = new Text(gameData, "Score:", transform.position - new Vector2(200.0f, 0.0f));
            title.transform.scale = transform.scale * 100.0f;
            title.color = color;
            title.Load();
        }

        public override void Update(GameTime gameTime)
        {
            ScoreValue = score.TotalScore;
            if (!isCalc) return;
            isCalc = false;

            text = ScoreValue.ToString().PadLeft(10, '0');
        }

        public override void Draw()
        {
            title.Draw();
            base.Draw();
        }
    }
}
