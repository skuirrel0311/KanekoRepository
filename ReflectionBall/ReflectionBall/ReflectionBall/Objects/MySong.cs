using Microsoft.Xna.Framework.Media;

namespace ReflectionBall
{
    public class MySong : GameObject2D
    {
        public Asset<Song> song { get; private set; }

        public MySong(string name, GameData gameData)
            : base(name, gameData)
        {

        }

        public override void Load()
        {
            song = gameData.songManager.GetContent(name);
        }

        public void Play(bool isLoop = true)
        {
            MediaPlayer.IsRepeating = isLoop;
            MediaPlayer.Play(song.data);
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            song.UnLoad();
        }
    }
}
