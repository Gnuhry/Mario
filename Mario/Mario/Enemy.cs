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
        private bool normal;
        public Enemy(bool normal)
        {
            InitializeComponent();
            Active = false;
            this.normal = normal;
            if (normal)
            {
                Size = new Size(Settings.width, 2 * Settings.height);
                Tag = "Enemy_pointed";
                pcBEnemy.Size = new Size(Settings.width, 2 * Settings.height); ;
                pcBEnemy.Image = Properties.Resources.big_ben_right;
            }
            else
            {
                Size = new Size(Settings.width, Settings.height);
                pcBEnemy.Image = Properties.Resources.grammy_right;
                Tag = "Enemy";
            }
            right = true;
            enemy_timer = new Timer
            {
                Interval = 100
            };
            enemy_timer.Tick += Enemy_timer_Tick;
        }

        public void Start(Players players)
        {
            Active = true;
            enemy_timer.Start();
            this.players = players;
        }
        public void Stop()
        {
            Active = false;
            enemy_timer.Stop();
        }
        private bool Active;
        public bool IsActive
        {
            get => Active;
        }
        private void Enemy_timer_Tick(object sender, EventArgs e)
        {
            Point help = Location;
            if (right)
            {
                if (CollisionDetect(new Point(Settings.speedX, 0)))
                {
                    help.Offset(Settings.speedX, 0);
                    if (CollisionDetect(new Point(0, Settings.speedY)))
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
                    if (CollisionDetect(new Point(0, Settings.speedY)))
                    {
                        help.Offset(0, Settings.speedY);
                        Console.WriteLine("down");
                        Location = help;
                    }
                    if (normal)
                    {
                        pcBEnemy.Image = Properties.Resources.big_ben_left;
                    }
                    else
                    {
                        pcBEnemy.Image = Properties.Resources.grammy_left;
                    }
                    return;
                }
            }
            else
            {
                if (CollisionDetect(new Point(-Settings.speedX, 0)))
                {
                    help.Offset(-Settings.speedX, 0);
                    if (CollisionDetect(new Point(0, Settings.speedY)))
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
                    if (CollisionDetect(new Point(0, Settings.speedY)))
                    {
                        help.Offset(0, Settings.speedY);
                        Console.WriteLine("down");
                        Location = help;
                    }
                    if (normal)
                    {
                        pcBEnemy.Image = Properties.Resources.big_ben_right;

                    }
                    else
                    {
                        pcBEnemy.Image = Properties.Resources.grammy_right;

                    }
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
                    else if (control.Tag.ToString().Equals("water"))
                    {
                            Hit();
                    }
                }
            }
            if (help.X < 0)
            {
                return false;
            }
            if (help.X > Screen.PrimaryScreen.WorkingArea.Width - Settings.width)
            {
                return false;
            }
            return true;
        }

        public void Hit(Settings settings)
        {
            Sound_music.HitEnemySound(settings);
            enemy_timer.Stop();
            Parent.Controls.Remove(this);
            Dispose();
        }
        private void Hit()
        {
            enemy_timer.Stop();
            Parent.Controls.Remove(this);
            Dispose();
        }



    }
}
