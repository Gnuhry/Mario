using System.Media;
namespace Mario
{

    public class sound_music
    {
        //TODO
        public static void RiceSound(Settings settings)
        {
            if (settings.sounds)
            {
                new SoundPlayer(Settings.music_sound_path + "rice.wav").Play();
                StartMusic(settings);
            }
        }
        //Zerteilen der Musik und nacheinander abspielen
        private static SoundPlayer music;
        public static void StartMusic(Settings settings)
        {
            if (settings.music)
            {
                music = new SoundPlayer(Settings.music_sound_path + "level1.wav");
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
