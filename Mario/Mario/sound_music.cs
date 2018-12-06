using System.Media;
using System.Windows.Media;

namespace Mario
{

    public class Sound_music
    {
        //TODO set sounds
        public static void RiceSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        private static MediaPlayer myPlayer;
        private static Settings settings_;
        public static void CheckMusic(Settings settings)
        {
            settings_ = settings;
            if (settings.Music)
            {
                myPlayer = new MediaPlayer();
                myPlayer.Open(new System.Uri(Settings.path + @"Resources\music_sound\level1.wav"));
                myPlayer.Play();
                myPlayer.MediaEnded += MyPlayer_MediaEnded;
                myPlayer.Volume = settings.Volume;
            }
            else
            {
                if (myPlayer != null)
                {
                    myPlayer.Stop();
                }
            }
        }

        private static void MyPlayer_MediaEnded(object sender, System.EventArgs e)
        {
            myPlayer.Open(new System.Uri(Settings.path + @"Resources\music_sound\level1.wav"));
            myPlayer.Play();
            myPlayer.Volume = settings_.Volume;
        }

        public static void ChangeVolume(Settings settings)
        {
            myPlayer.Volume = settings.Volume;
        }
    }
}
