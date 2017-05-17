using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ReflectionBall
{
    public class MySoundEffect : GameObject2D
    {
        public Asset<SoundEffect> soundEffect { get; private set; }

        public MySoundEffect(string name, GameData gameData)
            :base(name, gameData)
        {

        }

        public override void Load()
        {
            soundEffect = gameData.soundEffectManager.GetContent(name);
        }

        public void Play()
        {
            soundEffect.data.Play();
        }

        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="volume">音量(0～1)</param>
        /// <param name="pitch">ピッチ(-1～1)</param>
        /// <param name="pan">どちらの耳に聞こえるか(-1(左)～1(右))</param>
        public void Play(float volume,float pitch = 0.0f, float pan = 0.0f)
        {
            volume = MathHelper.Clamp(volume, 0.0f, 1.0f);
            pitch = MathHelper.Clamp(pitch, -1.0f, 1.0f);
            pan = MathHelper.Clamp(pan, -1.0f, 1.0f);
            soundEffect.data.Play(volume, pitch, pan);
        }

        public override void UnLoad()
        {
            soundEffect.UnLoad();
        }
    }
}
