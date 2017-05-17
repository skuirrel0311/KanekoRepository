using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace ReflectionBall
{
    public class TitleScene : Scene
    {
        MySoundEffect clickSE;

        public TitleScene(GameData gameData, string sceneName)
            :base(gameData, sceneName)
        {
            objList.Add(new Sprite("Sprite\\titleBG", gameData));
            clickSE = new MySoundEffect("Audio\\ok", gameData);
            bgm = new MySong("Audio\\bgm1", gameData);

            objList.Add(bgm);
            objList.Add(clickSE);
        }

        public override void Initialize()
        {
            bgm.Play();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (MyInputManager.IsJustMouseButtonDown(MouseButton.LeftButton))
            {
                clickSE.Play();
                gameData.sceneManager.ChangeScene("MainGameScene");
            }
            base.Update(gameTime);
        }
    }
}
