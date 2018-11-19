using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{

    public partial class Itembox : UserControl
    {
        private Image[] ItemPicture;
        private Random random;
        private bool active;
        public Itembox()
        {
            InitializeComponent();
            ItemPicture = new Image[] { Properties.Resources.rice, Properties.Resources.pepper,
                Properties.Resources.stone, Properties.Resources.stone, Properties.Resources.stone };
            random = new Random();
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
                randomNr = random.Next(1, ItemPicture.Length);
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
