using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Player
    {
        private Control control_;
        private int JumpCounter_ = Settings.jumpspeed;
        private bool jumping_ = false, jump_ = false, right_ = false, left_ = false;
        public Control control
        {
            get => control_;
            set => control_ = value;
        }
        public int JumpCounter
        {
            get => JumpCounter_;
            set => JumpCounter_ = value;
        }

        public bool jumping
        {
            get => jumping_;
            set => jumping_ = value;
        }
        public Rectangle rectangle
        {
            get => new Rectangle(control.Location, control.Size);
        }
        public bool right
        {
            get => right_;
            set => right_ = value;
        }
        public bool left
        {
            get => left_;
            set => left_ = value;
        }
        public bool jump
        {
            get => jump_;
            set
            {
                jump_ = value;
                if (!jump_) JumpCounter_ = 0;
            }
        }

        public Player(Control control)
        {
            control_ = control;
        }

        public void Bewegen(int x, int y)
        {
            Point location = control_.Location;
            location.Offset(x, y);
            control_.Location = location;
        }

    }
}
