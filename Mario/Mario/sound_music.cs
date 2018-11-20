using System.Media;
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
                StartMusic(settings);
            }
        }
        //TODO Zerteilen der Musik und nacheinander abspielen
        private static SoundPlayer music;
        public static void StartMusic(Settings settings)
        {
            if (settings.music)
            {
                music = new SoundPlayer(Properties.Resources.level1);
                music.PlayLooping();
            }
        }
        public static void StopMusic()
        {
            if (music != null)
            {
                music.Stop();
            }
        }
    }
}
