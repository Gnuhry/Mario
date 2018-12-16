using System.Media;
using System.Windows.Media;

namespace Mario
{

    public class Sound_music
    {
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
            {
                new SoundPlayer(Properties.Resources.itempickup).Play();
            }
        }
        public static void FireballSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.fireball1).Play();
            }
        }
        public static void BumerangSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.bumerang_music).Play();
            }
        }
        public static void DoubleJumpSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.jump1).Play();
            }
        }
        public static void StarSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.star1).Play();
            }
        }
        public static void GrowSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.growsound).Play();
            }
        }
        public static void DieSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.die_sound).Play();
            }
        }
        public static void HitBlockSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.hitblocksound).Play();
            }
        }
        public static void HitSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.hitsound).Play();
            }
        }
        public static void HitEnemySound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.hitenemysound).Play();
            }
        }
        public static void DestroyBlockSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.destroy_block_sound).Play();
            }
        }
        public static void WaterSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.watersound).Play();
            }
        }
        public static void CloudSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.cloud1).Play();
            }
        }
        public static void RiceCoinSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.ricecoinsound).Play();
            }
        }
        public static void ItemBoxSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.itemboxsound).Play();
            }
        }
        public static void CounterSound(Settings settings)
        {
            if (settings.Sounds)
            {
                new SoundPlayer(Properties.Resources.counter_sound).Play();
            }
        }
        private static MediaPlayer myPlayer;
        private static Settings settings_;
        public static void CheckMusic(Settings settings)
        {
            settings_ = settings;
            if (settings.Music)
            {
                if (myPlayer == null)
                {
                    myPlayer = new MediaPlayer();
                    myPlayer.Open(new System.Uri(Settings.path + @"Resources\music_sound\music.wav"));
                    myPlayer.Play();
                    myPlayer.MediaEnded += MyPlayer_MediaEnded;
                    myPlayer.Volume = settings.Volume;
                }
                else
                {
                    myPlayer.Play();
                }
            }
            else
            {
                if (myPlayer != null)
                {
                    myPlayer.Stop();
                    myPlayer.MediaEnded -= MyPlayer_MediaEnded;
                    myPlayer = null;
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
