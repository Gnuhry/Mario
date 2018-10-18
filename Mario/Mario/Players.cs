using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Players : UserControl
    {
        private Timer playerTimer;
        private bool doubleJump, jump, up, right, left, fireFlower, fireFlowerRight, doubleJumping;
        private int jumpCounter, status, invincibleCounter;
        private double fireFlowerCounter;
        private PictureBox fireBall;
        private Settings settings;
        public Players(Settings settings)
        {
            InitializeComponent();
            status = 1;
            Size = new Size(Settings.width, 2 * Settings.height);
            Tag = "players";
            fireBall = new PictureBox()
            {
                Image = Properties.Resources.fireball,
                Size = new Size(Settings.width, Settings.height),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.settings = settings;
            playerTimer = new Timer();
            KeyDown += Players_KeyDown;
            KeyUp += Players_KeyUp;
            playerTimer.Interval = Settings.timerlenght;
            playerTimer.Tick += Player_Move;
            playerTimer.Start();
        }

        private void Players_KeyUp(object sender, KeyEventArgs e)
        {
            char equal = Convert.ToChar(e.KeyValue);
            if (equal.Equals(settings.up))
            {
                up = false;
            }
            else if (equal.Equals(settings.left))
            {
                left = false;
            }
            else if (equal.Equals(settings.right))
            {
                right = false;
            }
        }

        private void Players_KeyDown(object sender, KeyEventArgs e)
        {
            char equal = Convert.ToChar(e.KeyValue);
            if (equal.Equals(settings.up))
            {
                up = true;
            }
            else if (equal.Equals(settings.left))
            {
                left = true;
            }
            else if (equal.Equals(settings.right))
            {
                right = true;
            }
            else if (equal.Equals(settings.item))
            {
                UseItem();
            }
        }

        public void SetPlayerTimer(bool on)
        {
            if (on) playerTimer.Start();
            else playerTimer.Stop();
        }

        public void UseItem()
        {
            if (fireFlower && fireFlowerCounter == 0)
            {
                Console.WriteLine("heyyyyyy");
                fireFlowerCounter = Settings.fireBallBlockLenght;
            }
        }
        bool upbefore = false;
        private void Player_Move(object sender, EventArgs e)
        {
            BringToFront(); Focus();
            Change();
            Point point = new Point();
            if (up && doubleJumping && !jump && !upbefore)
            {
                Console.WriteLine(doubleJumping);

                jump = true;
                jumpCounter = Settings.jumpspeed;
                doubleJumping = false;
            }
            if (up && jump)
            {
                if (jumpCounter-- <= 0)
                {
                    jump = false;
                }
                else
                {
                    if (CollisionDetect(new Point(0, -Settings.speedY)))
                    {
                        point.Offset(0, -Settings.speedY);
                    }
                    else
                    {
                        Point help = Location;
                        help.Offset(0, -Settings.speedY);
                        Rectangle rectangle;
                        switch (status)
                        {
                            case 1:
                                help.Offset(0, Settings.height);
                                rectangle = new Rectangle(help, new Size(Settings.width, Settings.height));
                                break;
                            default:
                                rectangle = new Rectangle(help, new Size(Settings.width, 2 * Settings.height));
                                break;
                        }
                        foreach (Control control in Parent.Controls)
                        {
                            if (control.GetType().Equals(typeof(Itembox)))
                            {
                                if (rectangle.IntersectsWith(new Rectangle(control.Location, control.Size)))
                                {
                                    PictureBox item = ((Itembox)control).Activate(status == 1);
                                    if (item != null)
                                    {
                                        Parent.Controls.Add(item);
                                        item.BringToFront();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                if (CollisionDetect(new Point(0, Settings.speedY)))
                {
                    jump = false;
                    point.Offset(0, Settings.speedY);
                }
                else
                {
                    jump = true;
                    jumpCounter = Settings.jumpspeed;
                    doubleJumping = doubleJump;
                }
            }

            if (right && left)
            {
                right = left = fireFlowerRight = false;
            }
            else if (right && CollisionDetect(new Point(Settings.speedX, 0)))
            {
                fireFlowerRight = true;
                point.Offset(Settings.speedX, 0);
            }
            else if (left && CollisionDetect(new Point(-Settings.speedX, 0)))
            {
                fireFlowerRight = false;
                point.Offset(-Settings.speedX, 0);
            }
            Point temp = Location;
            temp.Offset(point);
            Location = temp;


            if (invincibleCounter > 0)
                invincibleCounter--;
            FireFlower();
            upbefore = up;
        }

        private void FireFlower()
        {
            if (!fireFlower) return;
            Point help, offset;
            if (fireFlowerCounter == Settings.fireBallBlockLenght)
            {
                offset = new Point(0,0);
                help = Location;
                if (fireFlowerRight)
                {
                    help.Offset(Settings.width,0);
                }
                else
                {
                    help.Offset(-Settings.width, 0);
                }
                if (CollisionDetect(help))
                {
                    fireBall.Location = help;
                    Parent.Controls.Add(fireBall);
                    fireBall.BringToFront();
                    fireFlowerCounter--;
                }
                else
                {
                    fireFlowerCounter = 0;
                }
            }
            else if (fireFlowerCounter > 0)
            {
                offset = new Point();
                help = fireBall.Location;
                if (fireFlowerRight)
                {
                    offset.X = Settings.width;
                }
                else
                {
                    offset.X = -Settings.width;
                }
                help.Offset(offset);
                Console.WriteLine(help.ToString());
                if (CollisionDetect(help))
                {
                    fireBall.Location = help;
                    fireBall.BringToFront();
                    fireFlowerCounter--;
                }
                else
                {
                    fireFlowerCounter = 0;
                }
            }
            if (fireFlowerCounter == 0)
            {
                Parent.Controls.Remove(fireBall);
            }
        }

        private bool CollisionDetect(Point Offset)
        {
            Point help = Location;
            help.Offset(Offset);
            Rectangle rectangle;
            switch (status)
            {
                case 1:
                    help.Offset(0, Settings.height);
                    rectangle = new Rectangle(help, new Size(Settings.width, Settings.height));
                    break;
                default:
                    rectangle = new Rectangle(help, new Size(Settings.width, 2 * Settings.height));
                    break;
            }
            foreach (Control control in Parent.Controls)
            {
                if (control.Tag != null)
                {
                    if (control.Tag.Equals("obstacle") || control.GetType().Equals(typeof(Itembox)))
                    {
                        if (rectangle.IntersectsWith(new Rectangle(control.Location, control.Size)))
                        {
                            Console.WriteLine(rectangle.Location.ToString() + "==" + control.Location.ToString());
                            /*control.BackgroundImage = Properties.Resources.stone;
                                                       try
                                                       {
                                                           ((PictureBox)control).Image = Properties.Resources.stone;
                                                       }
                                                       catch (Exception) { }*/
                            return false;
                        }
                    }
                    else if (control.Tag.ToString().Split('_')[0].ToLower().Equals("item"))
                    {
                        if (rectangle.IntersectsWith(new Rectangle(control.Location, control.Size)))
                        {
                            Parent.Controls.Remove(control);
                            switch (control.Tag.ToString().Split('_')[1])
                            {
                                case "0":
                                    status = 0;
                                    fireFlower = false;
                                    invincibleCounter = 0;
                                    doubleJump = false;
                                    break;//Mushroom
                                case "1":
                                    fireFlower = true;
                                    invincibleCounter = 0;
                                    doubleJump = false;
                                    break;//Fire Flower
                                case "2":
                                    invincibleCounter = Settings.invincibleCounter;
                                    fireFlower = false;
                                    doubleJump = false;
                                    break;//Star
                                case "3":
                                    doubleJumping = true;
                                    doubleJump = true;
                                    fireFlower = false;
                                    invincibleCounter = 0;
                                    break;//Double Jump
                            }
                            return true;
                        }
                    }
                }
            }
            return true;
        }
        private void Change() //TODO Grafiken hinzufügen
        {
            if (status == 1)
            {
                if (right)
                {
                    if (fireFlower)
                    {
                        //2 Blöcke nach rechts feuerblume
                    }
                    else if (invincibleCounter > 0)
                    {
                        //2 Blöcke nach rechts stern
                    }
                    else if (doubleJump)
                    {
                        //2 Blöcke nach rechts doublejump
                    }
                    else
                    {
                        //2 Blöcke nach rechts 
                    }
                }
                else if (left)
                {
                    if (fireFlower)
                    {
                        //2 Blöcke nach links feuerblume
                    }
                    else if (invincibleCounter > 0)
                    {
                        //2 Blöcke nach links stern
                    }
                    else if (doubleJump)
                    {
                        //2 Blöcke nach links doublejump
                    }
                    else
                    {
                        //2 Blöcke nach links 
                    }
                }
                else
                {
                    if (fireFlower)
                    {
                        //2 Blöcke feuerblume
                    }
                    else if (invincibleCounter > 0)
                    {
                        //2 Blöcke stern
                    }
                    else if (doubleJump)
                    {
                        //2 Blöcke doublejump
                    }
                    else
                    {
                        //2 Blöcke 
                    }
                }
            }
            else if (status == 0)
            {
                if (right)
                {
                    //1 Blöcke nach rechts 
                }
                else if (left)
                {
                    //1 Blöcke nach links 
                }
                else
                {
                    //1 Blöcke 
                }
            }
        }
        public void Hit()
        {
            if (invincibleCounter > 0)
            {
                return;
            }
            if (fireFlower || doubleJump)
            {
                fireFlower = doubleJump = doubleJumping = false;
                return;
            }
            if (status != 1)
            {
                status = 1;
                return;
            }
            //Leben abziehen
        }
    }
}
