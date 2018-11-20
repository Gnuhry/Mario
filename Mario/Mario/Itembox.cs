using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{

    public partial class Itembox : UserControl
    {
        private Image[] ItemPicture;
        private bool active;
        public Itembox()
        {
            InitializeComponent();
            ItemPicture = new Image[] { Properties.Resources.rice, Properties.Resources.pepper,
                Properties.Resources.stone, Properties.Resources.springroll, Properties.Resources.dirt };
            Size = new Size(Settings.width, Settings.height);
            Tag = "itembox";
            active = true;
        }
        public PictureBox Activate(bool mushroom)
        {
            if (!active) return null;
            this.BackgroundImage = Properties.Resources.event_field_close;
            active = false;
            int randomNr;
            if (mushroom)
            {
                randomNr = 0;
            }
            else
            {
                randomNr = DateTime.Now.Millisecond;
                randomNr = (new Random().Next(1, ItemPicture.Length)* new Random().Next(1, ItemPicture.Length) + DateTime.Now.Millisecond);
                randomNr = randomNr % 4 + 1;
                if (randomNr == 2)
                {
                    randomNr += DateTime.Now.Millisecond;
                }
                randomNr = randomNr % 4 + 1;

            }
            PictureBox item = new PictureBox()
            {
                Tag = "Item_" + randomNr,
                Image = ItemPicture[randomNr],
                Size = Size,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(Location.X, Location.Y - Settings.height)
            };
            Parent.Controls.Add(item);
            item.BringToFront();
            return item;
        }
        public void Reset()
        {
            active = true;
        }
    }
}
