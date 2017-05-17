using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class ResultScene : Scene
    {
        Text[] textArray = new Text[4];
        MySoundEffect don, ddn, jan;
        Text exitText;

        Timer intervalTimer;
        int currentIndex = 0;

        public ResultScene(GameData gameData, string sceneName)
            : base(gameData, sceneName)
        {
            //textを表示させる間隔
            intervalTimer = new Timer();
            intervalTimer.Start(1.0f);
            objList.Add(intervalTimer);

            float screenOriginX = GameData.windowWidth * 0.5f;

            textArray[0] = new Text(
                gameData, "reflection : " + gameData.score.reflectCount.ToString().PadLeft(3, '0'),
                new Vector2(screenOriginX, 100.0f)
            );

            textArray[1] = new Text(
                gameData, "max speed : " + gameData.score.maxSpeed.ToString().PadLeft(3, '0'),
                new Vector2(screenOriginX, 200.0f)
            );

            textArray[2] = new Text(
                gameData, "bounus : " + gameData.score.speedBounus.ToString().PadLeft(6, '0'),
                new Vector2(screenOriginX, 300.0f)
            );

            textArray[3] = new Text(
                gameData, "total score : " + gameData.score.TotalScore.ToString().PadLeft(9, '0'),
                new Vector2(screenOriginX, 400.0f)
            );

            foreach (Text t in textArray)
            {
                t.transform.scale = 20.0f;
                t.isDraw = false;
                objList.Add(t);
            }

            exitText = new Text(gameData, "click -> skip", new Vector2(1000.0f, 600.0f));
            objList.Add(exitText);

            don = new MySoundEffect("Audio\\don", gameData);
            objList.Add(don);

            ddn = new MySoundEffect("Audio\\ddn", gameData);
            objList.Add(ddn);

            jan = new MySoundEffect("Audio\\zya-n", gameData);
            objList.Add(jan);
        }

        public override void Initialize()
        {
            ddn.Play();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (MyInputManager.IsJustMouseButtonDown(MouseButton.LeftButton))
            {
                if (textArray[textArray.Length - 1].isDraw)
                    gameData.sceneManager.ChangeScene("TitleScene");
                else
                    Skip();
            }

            if (intervalTimer.isLimitTime)
            {
                textArray[currentIndex].isDraw = true;
                currentIndex++;
                if (currentIndex >= textArray.Length)
                {
                    jan.Play();
                    intervalTimer.Stop();
                    return;
                }
                else
                {
                    don.Play();
                }
                intervalTimer.ReStart();
            }

            if (!intervalTimer.isStart)
            {
                exitText.text = "click -> continue";
            }
            base.Update(gameTime);
        }

        public override void UnloadContent()
        {
            gameData.score.Unload();
            base.UnloadContent();
        }

        void Skip()
        {
            foreach (Text t in textArray)
            {
                t.isDraw = true;
            }
            intervalTimer.Stop();
            jan.Play();
        }
    }
}
