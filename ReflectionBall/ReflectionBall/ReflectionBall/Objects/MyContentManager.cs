using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ReflectionBall
{
    /// <summary>
    /// 一つのコンテンツにつきLoadは１回しかしない
    /// </summary>
    public class MYContentManager<T> where T : class
    {
        public static MYContentManager<T> I;

        ContentManager content;

        List<Asset<T>> contentList = new List<Asset<T>>();

        public MYContentManager(Game game)
        {
            I = this;
            content = game.Content;
        }

        public virtual Asset<T> GetContent(string name)
        {
            Asset<T> asset = contentList.Find(n => n.name == name);

            if (asset != null) return asset;

            //まだ読み込まれていなかった
            asset = new Asset<T>(name, content);
            contentList.Add(asset);

            return asset;
        }

        public void UnLoadContent(string name)
        {
            Asset<T> asset = contentList.Find(n => n.name == name);
            if (asset == null) return;

            contentList.Remove(asset);
            asset = null;
        }
    }
}