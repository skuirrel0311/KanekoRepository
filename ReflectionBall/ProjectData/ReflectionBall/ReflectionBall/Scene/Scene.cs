using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class Scene
    {
        public string sceneName;
        protected GameData gameData;

        protected MySong bgm;

        public List<GameObject2D> objList = new List<GameObject2D>();

        List<GameObject2D> currentInstantiateObj = new List<GameObject2D>();
        List<GameObject2D> currentDestroyObj = new List<GameObject2D>();

        public Scene(GameData gameData, string sceneName)
        {
            this.gameData = gameData;
            this.sceneName = sceneName;
        }
        
        public virtual void LoadContent()
        {
            foreach(GameObject2D g in objList)
            {
                g.Load();
            }
        }

        public virtual void Initialize()
        {
            foreach (GameObject2D g in objList)
            {
                g.Initialize();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            Instantiate();
            Destroy();

            foreach (GameObject2D g in objList)
            {
                g.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {

            gameData.spriteBatch.Begin();
            foreach (GameObject2D g in objList)
            {
                g.Draw();
            }
            gameData.spriteBatch.End();
        }

        public virtual void UnloadContent()
        {
            foreach (GameObject2D g in objList)
            {
                g.UnLoad();
            }
        }

        public void Instantiate(GameObject2D obj)
        {
            currentInstantiateObj.Add(obj);
        }
        void Instantiate()
        {
            foreach (GameObject2D g in currentInstantiateObj)
            {
                g.Load();
                g.Initialize();
                objList.Add(g);
            }

            currentInstantiateObj.Clear();
        }

        public void Destroy(GameObject2D obj)
        {
            currentDestroyObj.Add(obj);
        }
        void Destroy()
        {
            foreach (GameObject2D g in currentDestroyObj)
            {
                g.UnLoad();
                objList.Remove(g);
            }

            currentDestroyObj.Clear();
        }
    }
}
