using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public class Items
    {
        private string fireballPath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "img\\fireball.jpg";
        private List<Control> item_control;
        private List<int> item_value;
        private bool mushroom_; //TODO
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

        public bool mushroom //TODO
        {
            get => mushroom_;
            set => mushroom_ = value;
        }



        public Items()
        {
            jumpCounter = 0;
            item_ = 1;
        }

        public Rectangle GetRandomItem(Control.ControlCollection controlCollection, Point item_location)
        {
            Random rnd = new Random();
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(Settings.width, Settings.hight);
            int item_rnd = rnd.Next(1, 4);
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
            item_ = item_value[id];
            item_value.RemoveAt(id);
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
            string path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "img\\";
            switch (item_)
            {
                case 1:
                    return path + "doublejump.jpg";//DoubleJump
                case 2:
                    return path + "fireflower.jpg";//FireFlower
                case 3:
                    return path + "invincible.jpg";//Invincible
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
                Image = Image.FromFile(Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "img\\fireball_throw.jpg")
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
