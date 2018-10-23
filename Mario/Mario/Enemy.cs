using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class Enemy : UserControl
    {
        private Timer enemy_timer;
        private bool right;
        private Players players;
        public Enemy()
        {
            InitializeComponent();
            Tag = "Enemy";
            Size = new Size(Settings.width, Settings.height);
            right = true;
            enemy_timer = new Timer();
            enemy_timer.Interval = 100;
            enemy_timer.Tick += Enemy_timer_Tick;
        }
        public void Start(Players players)
        {
            enemy_timer.Start();
            this.players = players;
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
                if (CollisionDetect(new Point(Settings.speedX,0)))
                {
                    help.Offset(Settings.speedX, 0);
                    if (CollisionDetect(new Point(0,Settings.speedY)))
                    {
                        help.Offset(0, Settings.speedY);
                        Console.WriteLine("Right down");
                        Location = help;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Right");
                        Location = help;
                        return;
                    }
                }
                else
                {
                    right = false;
                    pcBEnemy.Image = Properties.Resources.enemy_left;
                    return;
                }
            }
            else
            {
                if (CollisionDetect(new Point(-Settings.speedX,0)))
                {
                    help.Offset(-Settings.speedX, 0);
                    if (CollisionDetect(new Point(0,Settings.speedY)))
                    {
                        help.Offset(0, Settings.speedY);
                        Console.WriteLine("LEft down");
                        Location = help;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Left");
                        Location = help;
                        return;
                    }
                }
                else
                {
                    right = true;
                    pcBEnemy.Image = Properties.Resources.enemy_right;
                    return;
                }
            }
        }

        private bool CollisionDetect(Point Offset)
        {
            //Rectangle rectangle = Bounds;
            //rectangle.Offset(Offset);
            //return players.CollisionDetect(rectangle, false, false, false, false);
           if (Parent == null) return false;
            Point help = Location;
            help.Offset(Offset);
            Rectangle rectangle = Bounds;
            rectangle.Offset(Offset);
            foreach (Control control in Parent.Controls)
            {
                if (control.Bounds.IntersectsWith(rectangle) && control.Tag != null)
                {
                    if (control.Tag.ToString().Split('_')[0].Equals("obstacle") || control is Itembox)
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
