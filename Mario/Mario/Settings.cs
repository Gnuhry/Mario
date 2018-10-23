using System.Drawing;

namespace Mario
{
    public class Settings
    {
        public static int
            speedX = 10,
            speedY = 10,
            jumpspeed = 20,
            width = 50,
            height = 50,
            timerlenght = 50,
            gamehight = 20,
            itemThrowBlockLength = 3,
            invincibleCounter = 100;
        public static Size size = new Size(width, height);
        public static double borderFactor=0.1;
        private char up_, left_, right_, item_;
        private bool music_, sounds_;
        public char up
        {
            get => up_;
            set => up_ = value;
        }
        public bool music
        {
            get => music_;
            set => music_ = value;
        }
        public bool sounds
        {
            get => sounds_;
            set => sounds_ = value;
        }
        public char left
        {
            get => left_;
            set => left_ = value;
        }
        public char right
        {
            get => right_;
            set => right_ = value;
        }
        public char item
        {
            get => item_;
            set => item_ = value;
        }

        public Settings()
        {
            Default();
        }
        public void Default()
        {
            up_ = 'W';
            left_ = 'A';
            right_ = 'D';
            item_ = 'Q';
            music_ = sounds_ = true;
        }
    }
}
