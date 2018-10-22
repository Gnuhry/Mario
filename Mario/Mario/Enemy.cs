using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class Enemy : UserControl
    {
        private Timer enemy_timer;
        private bool right;
        public Enemy()
        {
            InitializeComponent();
            Tag = "Enemy";
            Size = new Size(Settings.width, Settings.height);
            right = true;
            enemy_timer = new Timer();
            enemy_timer.Interval = 50;
            enemy_timer.Tick += Enemy_timer_Tick;
        }
        public void Start()
        {
            enemy_timer.Start();
        }
        public void Stop()
        {
            enemy_timer.Stop();
        }
        private void Enemy_timer_Tick(object sender, EventArgs e)
        {
            Point help = Location;
            if (right)
            {
                help.Offset(Settings.speedX, 0);
                if (CollisionDetect(help))
                {
                    help.Offset(0, Settings.speedY);
                    if (CollisionDetect(help))
                    {
                        Console.WriteLine("Right down");
                        Location = help;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Right");
                        help.Offset(0, -Settings.speedY);
                        Location = help;
                        return;
                    }
                }
                else
                {
                    right = false;
                }
            }
            else
            {
                help.Offset(-Settings.speedX, 0);
                if (CollisionDetect(help))
                {
                    help.Offset(0, Settings.speedY);
                    if (CollisionDetect(help))
                    {
                        Console.WriteLine("LEft down");
                        Location = help;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Left");
                        help.Offset(0, -Settings.speedY);
                        Location = help;
                        return;
                    }
                }
                else
                {
                    right = true;
                }
            }
            help = Location;
            help.Offset(0, -Settings.speedY);
            if (CollisionDetect(help))
            {
                Location = help;
            }
        }

        private bool CollisionDetect(Point Offset)
        {
            if (Parent == null) return false;
            Point help = Location;
            help.Offset(Offset);
            Rectangle rectangle = Bounds;
            rectangle.Offset(Offset);
            foreach (Control control in Parent.Controls)
            {
                if (control.Bounds.IntersectsWith(rectangle) && control.Tag != null)
                {
                    if (control.Tag.Equals("obstacle") || control.GetType().Equals(typeof(Itembox)))
                    {
                        return false;
                    }
                }
            }
            if (help.X < 0)
            {
                return false;
            }
            if (help.X > Screen.PrimaryScreen.WorkingArea.Width)
            {
                return false;
            }
            return true;
        }

        public void Hit()
        {
            enemy_timer.Stop();
            Parent.Controls.Remove(this);
            Dispose();
        }


    }
}
