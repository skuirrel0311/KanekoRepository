using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ReflectionBall
{
    public class Asset<T> where T : class
    {
        public string name;
        public T data { get; private set; }

        public Asset(string name, ContentManager content)
        {
            this.name = name;

            try
            {
                data = content.Load<T>(name);
            }
            catch
            {
                data = null;
                Debug.Log(name + "is not found or load error");
            }
        }

        public virtual void UnLoad()
        {
            MYContentManager<T>.I.UnLoadContent(name);
        }
    }
}
