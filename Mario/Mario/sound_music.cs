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
        public static void PickItemSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void FireballSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void BumerangSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void DoubleJumpSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void StarSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void GrowSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void DieSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void HitBlockSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void HitSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void HitEnemySound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void DestroyBlockSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void WaterSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void CloudSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void RiceCoinSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void ItemBoxSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
                new SoundPlayer(Properties.Resources.coin).Play();
            }
        }
        public static void CounterSound(Settings settings)
        {
            if (settings.Sounds)
            {//TODO
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
