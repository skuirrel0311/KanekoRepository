using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReflectionBall
{
    public class NotificationManager : GameObject2D
    {
        public NotificationManager(GameData gameData)
            :base("NotificationManager", gameData)
        {
        }

        /// <summary>
        /// ゲーム画面にメッセージを表示します
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="viewTime">完全に表示されてから表示されている時間</param>
        /// <param name="cameTime">完全に表示されるのにかかる時間</param>
        /// <param name="outTime">表示されてから消えるまでの時間</param>
        public NotificationMessage ShowMessage(string message,Vector2 position, float viewTime = 2.0f, float cameTime = 0.3f, float outTime = 0.5f)
        {
            NotificationMessage temp = new NotificationMessage(gameData, message,position, cameTime, viewTime, outTime);
            gameData.sceneManager.currentScene.Instantiate(temp);

            return temp;
        }
    }
}
