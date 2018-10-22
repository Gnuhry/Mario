using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class Players : UserControl
    {
        private Timer playerTimer;
        private bool doubleJump, jump, up, right, left, fireball, itemThrowRight, doubleJumping, bumarang;
        private int jumpCounter, status, invincibleCounter;
        private double itemThrowCounter;
        private PictureBox itemFly;
        private Settings settings;
        private pause pause;
        public Players(Settings settings)
        {
            InitializeComponent();
            status = 1;
            Size = new Size(Settings.width, 2 * Settings.height);
            BackColor = Color.Blue;
            Tag = "players";
            itemFly = new PictureBox()
            {
                Size = new Size(Settings.width, Settings.height),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.settings = settings;
            playerTimer = new Timer();
            KeyDown += Players_KeyDown;
            KeyUp += Players_KeyUp;
            KeyPress += Players_KeyPress;
            playerTimer.Interval = Settings.timerlenght;
            playerTimer.Tick += Player_Move;
            playerTimer.Start();
        }
        private void Players_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                pause = new pause(Parent as Play,(Parent as Play).GetSettings(), (Parent as Play).GetWorlds());
                pause.Show();
                (Parent as Form).Enabled = false;
                pause.FormClosed += Pause_FormClosed;
                playerTimer.Stop();
                jump = false;
                left = false;
                right = false;
            }
        }

        private void Pause_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Parent != null)
            {
                Parent.Enabled = true;
                playerTimer.Start();
            }
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
            if (fireball && itemThrowCounter == 0)
            {
                itemThrowCounter = Settings.itemThrowBlockLength;
            }
            else if (bumarang && itemThrowCounter == 0)
            {
                itemThrowCounter = Settings.itemThrowBlockLength;
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
                    if (CollisionDetect(new Point(0, -Settings.speedY), true))
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
                if (CollisionDetect(new Point(0, Settings.speedY), true))
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
                right = left = itemThrowRight = false;
            }
            else if (right && CollisionDetect(new Point(Settings.speedX, 0), true))
            {
                itemThrowRight = true;
                point.Offset(Settings.speedX, 0);
            }
            else if (left && CollisionDetect(new Point(-Settings.speedX, 0), true))
            {
                itemThrowRight = false;
                point.Offset(-Settings.speedX, 0);
            }
            Point temp = Location;
            temp.Offset(point);
            Location = temp;


            if (invincibleCounter > 0)
                invincibleCounter--;
            ItemThrow();
            upbefore = up;
        }

        private void ItemThrow()
        {
            if (!(fireball || bumarang)) return;
            Point help, offset;
            if (itemThrowCounter == Settings.itemThrowBlockLength)
            {
                itemThrowCounter--;
                offset = new Point(0, 0);
                help = Location;
                if (itemThrowRight)
                {
                    help.Offset(Settings.width, 0);
                }
                else
                {
                    help.Offset(-Settings.width, 0);
                }
                Console.WriteLine(help.ToString());
                if (CollisionDetect(help, false))
                {
                    itemFly.Location = help;
                    Parent.Controls.Add(itemFly);
                    itemFly.BringToFront();
                }
                else
                {
                    itemThrowCounter = 0;
                }
            }
            else if (itemThrowCounter > 0)
            {
                offset = new Point();
                help = itemFly.Location;
                Point temp = itemFly.Location;
                temp.Offset(0, Settings.height);
                if (fireball && CollisionDetect(temp, false))
                {
                    offset.Y = Settings.height;
                }
                if (itemThrowRight)
                {
                    offset.X = Settings.width;
                }
                else
                {
                    offset.X = -Settings.width;
                }
                help.Offset(offset);
                Console.WriteLine(help.ToString());
                if (CollisionDetect(help, false))
                {
                    itemFly.Location = help;
                    itemFly.BringToFront();
                    //TODO Gegner/Münzen aufsammeln
                    itemThrowCounter--;
                }
                else
                {
                    itemThrowCounter = 0;
                }
            }
            if (itemThrowCounter == 0)
            {
                Parent.Controls.Remove(itemFly);
            }
        }

        private bool CollisionDetect(Point Offset, bool Player)
        {
            if (Parent == null) return false;
            Point help = Location;
            help.Offset(Offset);
            Rectangle rectangle;
            if (Player)
            {
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
            }
            else
            {
                rectangle = new Rectangle(Offset, new Size(Settings.width, Settings.height));
            }
            foreach (Control control in Parent.Controls)
            {
                if (control.Tag != null)
                {
                    if (control.Tag.Equals("obstacle") || control.GetType().Equals(typeof(Itembox)))
                    {
                        if (rectangle.IntersectsWith(new Rectangle(control.Location, control.Size)))
                        {
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
                                    fireball = false;
                                    invincibleCounter = 0;
                                    doubleJump = false;
                                    bumarang = false;
                                    break;//Mushroom
                                case "1":
                                    fireball = true;
                                    invincibleCounter = 0;
                                    doubleJump = false;
                                    bumarang = false;
                                    itemFly.Image = Properties.Resources.fireball;
                                    break;//Fire Flower
                                case "2":
                                    invincibleCounter = Settings.invincibleCounter;
                                    fireball = false;
                                    doubleJump = false;
                                    bumarang = false;
                                    break;//Star
                                case "3":
                                    doubleJumping = true;
                                    doubleJump = true;
                                    fireball = false;
                                    invincibleCounter = 0;
                                    bumarang = false;
                                    break;//Double Jump
                                case "4":
                                    doubleJumping = false;
                                    doubleJump = false;
                                    fireball = false;
                                    invincibleCounter = 0;
                                    bumarang = true;
                                    itemFly.Image = Properties.Resources.bumarang;
                                    break;//Bumarang
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
                    if (fireball)
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
                    else if (bumarang)
                    {
                        //2 Blöcke nach rechts bumerang
                    }
                    else
                    {
                        //2 Blöcke nach rechts 
                    }
                }
                else if (left)
                {
                    if (fireball)
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
                    else if (bumarang)
                    {
                        //2 Blöcke nach links bumerang
                    }
                    else
                    {
                        //2 Blöcke nach links 
                    }
                }
                else
                {
                    if (fireball)
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
                    else if (bumarang)
                    {
                        //2 Blöcke bumerang
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
            if (fireball || doubleJump)
            {
                fireball = doubleJump = doubleJumping = false;
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
