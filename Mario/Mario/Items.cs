using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public class Items
    {
        private List<Control> item_control;
        private List<int> item_value;
        private bool Mushroom_;
        private int Item_, JumpCounter;

        public int Item
        {
            get => Item_;
            set => Item_ = value;
        }

        public Rectangle getRandomItem(Control.ControlCollection controlCollection, Point item_location)
        {
            Random rnd = new Random();
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(Settings.width, Settings.hight);
            int item_rnd = rnd.Next(1, 4);
            pictureBox.Image = Image.FromFile(getItemPicture(item_rnd));
            item_location.Offset(0, -2 * Settings.speedX);
            pictureBox.Location = item_location;
            controlCollection.Add(pictureBox);
            item_control.Add(pictureBox);
            item_value.Add(item_rnd);
            return new Rectangle(item_location, new Size(Settings.width, Settings.hight));
        }
        public void pickUpItem(int id, Control.ControlCollection controlCollection)
        {
            controlCollection.Remove(item_control[id]);
            item_control.RemoveAt(id);
            Item_ = item_value[id];
            item_value.RemoveAt(id);
        }

        private string getItemPicture(int Item_)
        {
            string path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "img\\";
            switch (Item_)
            {
                case 1: return path + "doublejump.jpg";//DoubleJump
                case 2: return path + "fireflower.jpg";//FireFlower
                case 3: return path + "invincible.jpg";//Invincible
            }
            return null;
        }
        public bool FireFlower
        {
            get => Item_ == 2;
        }
        public bool Invincible
        {
            get => Item_ == 3;
        }
        public bool DoubleJump
        {
            get => Item_ == 1 && (JumpCounter++) % 2 == 0;
            set => JumpCounter = 0;
        }

        public bool Mushroom
        {
            get => Mushroom_;
            set => Mushroom_ = value;
        }



        public Items()
        {
            JumpCounter = 0;
            Item_ = 1;
        }
    }
}
