using System.Media;
using System.Windows.Media;

namespace Mario
{

    public class sound_music
    {
        //TODO set sounds
        public static void RiceSound(Settings settings)
        {
            if (settings.sounds)
            {
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        private static MediaPlayer myPlayer;
        public static void CheckMusic(Settings settings)
        {
            if (settings.music)
            {
                myPlayer = new MediaPlayer();
                myPlayer.Open(new System.Uri(Settings.path+ @"Resources\music_sound\level1.wav"));
                myPlayer.Play();
                myPlayer.Volume = settings.volume;
            }
            else
            {
                if (myPlayer != null)
                {
                    myPlayer.Stop();
                }
            }
        }
        public static void ChangeVolume(Settings settings)
        {
            myPlayer.Volume = settings.volume;
        }
    }
}
