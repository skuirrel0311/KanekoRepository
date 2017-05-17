using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class MainGameScene : Scene
    {
        BallManager ballManager;
        float intervalTime = 10.0f;

        Timer intervalTimer, readyTimer, gameTimer;
        TimeText timeText;

        public MainGameScene(GameData gameData, string sceneName)
            :base(gameData, sceneName)
        {
            bgm = new MySong("Audio\\bgm2", gameData);
            objList.Add(bgm);

            timeText = new TimeText(gameData, new Vector2(120.0f, 50.0f));
            timeText.transform.scale = 30.0f;
            timeText.color = Color.DarkSlateGray;
            objList.Add(timeText);

            ScoreText scoreText = new ScoreText(gameData, new Vector2(1000.0f, 50.0f),gameData.score);
            scoreText.transform.scale = 30.0f;
            scoreText.color = Color.DarkSlateGray;
            objList.Add(scoreText);

            //StageはUpdateしないのでListに追加しない
            Stage stage = new Stage("stage", gameData);

            ballManager = new BallManager(gameData, this, stage);
            objList.Add(ballManager);

            //一定間隔でボールを生成する(いつでも変更可)
            intervalTimer = new Timer(10.0f);
            objList.Add(intervalTimer);

            //ゲーム開始を少し遅らせる
            readyTimer = new Timer(3.0f);
            objList.Add(readyTimer);

            gameTimer = new Timer(GameData.LimitTime);
            objList.Add(gameTimer);
        }

        public override void Initialize()
        {
            bgm.Play();
            readyTimer.ReStart();

            NotificationMessage readyMessage = gameData.notificationManager.ShowMessage(
                "Ready!", new Vector2(GameData.windowWidth * 0.5f, GameData.windowHeight * 0.5f),
                readyTimer.limitTime - 0.8f
            );
            readyMessage.transform.scale = 200.0f;
            readyMessage.targetColor = Color.PaleGreen;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsGameEnd())
            {
                gameData.sceneManager.ChangeScene("ResultScene");
            }
            if (readyTimer.isLimitTime)
            {
                GameStart();
                return;
            }

            timeText.Time = gameTimer.limitTime - gameTimer.currentTime;

            if (intervalTimer.isLimitTime)
            {
                //生成に失敗したら普段より短い間隔で再度挑戦
                if (ballManager.GenerateBall(500.0f, true))
                    intervalTimer.Start(intervalTime);
                else
                    intervalTimer.Start(intervalTime * 0.3f);
            }
            base.Update(gameTime);
        }

        void GameStart()
        {
            NotificationMessage m = gameData.notificationManager.ShowMessage(
                "GO!", new Vector2(GameData.windowWidth * 0.5f, GameData.windowHeight * 0.5f),
                1.0f
            );
            m.transform.scale = 200.0f;
            m.targetColor = Color.PaleVioletRed;

            readyTimer.Stop();

            ballManager.GenerateBall(500.0f);
            intervalTimer.ReStart();
            gameTimer.ReStart();
        }

        bool IsGameEnd()
        {
            if (gameTimer.isLimitTime) return true;
            return false;
        }
    }
}
