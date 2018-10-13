using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Items
    {
        private string fireballPath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "img\\fireball.jpg";
        private List<Control> item_control;
        private List<int> item_value;
        private bool mushroom_;
        private int item_, jumpCounter;
        private Timer invincibleCounter;

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
        public void UseItem(Point player_location)
        {
            switch (item_)
            {
                case 2:
                    //TODO
                    //Kollisionsüberprüfung
                    //Feuerball plazieren
                    break;
            }
        }
    }
}
