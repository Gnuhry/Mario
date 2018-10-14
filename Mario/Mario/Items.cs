using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public class Items
    {
        private List<Control> item_control;
        private List<int> item_value;
        private bool mushroom_;
        private int item_, jumpCounter;
        private System.Windows.Forms.Timer invincibleCounter;

        public int item
        {
            get => item_;
            set => item_ = value;
        }
        public bool fireFlower
        {
            get => item_ == 2;
        }
        public bool invincible
        {
            get => item_ == 3;
        }
        public bool doubleJump
        {
            get => item_ == 1 && (jumpCounter++) % 2 == 0;
            set => jumpCounter = 0;
        }

        public bool mushroom
        {
            get => mushroom_;
            set => mushroom_ = value;
        }



        public Items()
        {
            invincibleCounter = new System.Windows.Forms.Timer();
            item_value = new List<int>();
            item_control = new List<Control>();
            jumpCounter = 0;
            item_ = 1;
        }

        public Rectangle GetRandomItem(Control.ControlCollection controlCollection, Point item_location)
        {
            Random rnd = new Random();
            int item_rnd = rnd.Next(1, 4);
            if (!mushroom_)
            {
                item_rnd = -1;
            }
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(Settings.width, Settings.hight);
            pictureBox.Image = Image.FromFile(GetItemPicture(item_rnd));
            item_location.Offset(0, -2 * Settings.speedX);
            pictureBox.Location = item_location;
            controlCollection.Add(pictureBox);
            item_control.Add(pictureBox);
            item_value.Add(item_rnd);
            return new Rectangle(item_location, new Size(Settings.width, Settings.hight));
        }
        public void PickUpItem(int id, Control.ControlCollection controlCollection)
        {
            controlCollection.Remove(item_control[id]);
            item_control.RemoveAt(id);
            if (item_value[id] != -1)
            {
                item_ = item_value[id];
                item_value.RemoveAt(id);
            }
            else
            {
                mushroom_ = true;
                item_value.RemoveAt(id);
                return;
            }
            if (item_ == 3)
            {
                invincibleCounter.Interval = 10000;
                invincibleCounter.Tick += InvincibleCounter_Tick;
                invincibleCounter.Start();
            }
        }

        private void InvincibleCounter_Tick(object sender, EventArgs e)
        {
            item_ = 0;
            invincibleCounter.Stop();
        }

        private string GetItemPicture(int item_)
        {
            string path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9);
            switch (item_)
            {
                case -1:
                    return path + Settings.imgMushroom;
                case 1:
                    return path + Settings.imgDoublejump;
                case 2:
                    return path + Settings.imgFireflower;
                case 3:
                    return path + Settings.imgInvincible;
            }
            return null;
        }
        public void UseItem(Point player_location, Control.ControlCollection controlCollection, Engine engine, bool right)
        {
            switch (item_)
            {
                case 2:
                    FireBall fireBall = new FireBall();
                    Thread thread = new Thread(fireBall.UseFireBall);
                    fireBall.controlCollection = controlCollection;
                    fireBall.engine = engine;
                    fireBall.player_location = player_location;
                    fireBall.right = right;
                    thread.Start();
                    break;
            }
        }
        public bool Hit()//return ISDead
        {
            if (item_ == 3)
            {
                return false;
            }
            if (item_ != 0)
            {
                item = 0;
                return false;
            }
            if (mushroom_)
            {
                mushroom_ = false;
                return false;
            }
            return true;
        }
    }
    class FireBall
    {
        public Control.ControlCollection controlCollection;
        public Engine engine;
        public Point player_location;
        public bool right;
        private PictureBox fireball;

        public void UseFireBall()
        {
            fireball = new PictureBox()
            {
                Size = new Size(Settings.width, Settings.hight),
                Image = Image.FromFile(Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + Settings.imgFireballThrow)
            };
            Rectangle rectangle = new Rectangle(player_location, new Size(Settings.width, Settings.hight)), down;
            for (int f = 0; f < 3; f++)
            {

                if (right)
                {
                    rectangle.Offset(0, Settings.speedY);
                }
                else
                {
                    rectangle.Offset(0, -Settings.speedY);
                }
                down = rectangle;
                down.Offset(Settings.speedX, 0);
                if (engine.CollisionDetect(down))
                {
                    if (f != 0)
                    {
                        controlCollection.Remove(fireball);
                    }
                    fireball.Location = down.Location;
                    controlCollection.Add(fireball);
                }
                else if (engine.CollisionDetect(rectangle))
                {
                    if (f != 0)
                    {
                        controlCollection.Remove(fireball);
                    }
                    fireball.Location = rectangle.Location;
                    controlCollection.Add(fireball);
                }
                else
                {
                    controlCollection.Remove(fireball);
                    return;
                }
                try
                {
                    Thread.Sleep(500);
                }
                catch (Exception) { }
            }
            controlCollection.Remove(fireball);
        }
    }
}
