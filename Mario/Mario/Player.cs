using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Player
    {
        private Control control_;
        private Items items;
        private int jumpCounter_ = Settings.jumpspeed, lives_;
        private bool jumping_ = false, jump_ = false, right_ = false, left_ = false;
        public Control control
        {
            get => control_;
            set => control_ = value;
        }
        public int jumpCounter
        {
            get => jumpCounter_;
            set => jumpCounter_ = value;
        }
        public int lives
        {
            get => lives_;
            set => lives_ = value;
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
                if (!jump_ && !items.doubleJump)
                {
                    jumpCounter_ = 0;
                }
            }
        }

        public Player(Control control, Items items)
        {
            control_ = control;
            this.items = items;
        }

        public void Move(int x, int y)
        {
            Point location = control_.Location;
            location.Offset(x, y);
            control_.Location = location;
        }

    }
}
