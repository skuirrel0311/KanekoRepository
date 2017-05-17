using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ReflectionBall
{
    public class SceneManager : DrawableGameComponent
    {
        List<Scene> sceneList = new List<Scene>();
        public Scene currentScene { get; private set; }
        GameData gameData;
        Assembly assembly;

        public SceneManager(GameData gameData)
            : base(gameData.game)
        {
            this.gameData = gameData;
            assembly = Assembly.GetExecutingAssembly();
        }

        public bool ChangeScene(string nextSceneName)
        {
            Scene next = GetSceneInstance(nextSceneName);

            if (next == null) return false;

            if (currentScene != null)
            {
                currentScene.UnloadContent();
            }

            currentScene = next;

            currentScene.LoadContent();
            currentScene.Initialize();

            return true;
        }

        Scene GetSceneInstance(string sceneName)
        {
            Scene scene = (Scene)assembly.CreateInstance(
                "ReflectionBall." + sceneName,
                false,
                BindingFlags.CreateInstance,
                null,
                new object[] { gameData, sceneName },
                null,
                null
            );
            return scene;
        }

        public override void Update(GameTime gameTime)
        {
            if (currentScene == null) return;

            currentScene.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (currentScene == null) return;

            currentScene.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
