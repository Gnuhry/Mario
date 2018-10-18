using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{

    public partial class Itembox : UserControl
    {
        private string[] ItemName;
        private Image[] ItemPicture;
        private Random random;
        private bool active;
        public Itembox()
        {
            InitializeComponent();
            ItemName = new string[] { "Mushroom", "FireFlower", "Star", "DoubleJump", "Bumarang" };
            ItemPicture = new Image[] { Properties.Resources.mushroom, Properties.Resources.fireflower, Properties.Resources.star, Properties.Resources.doubleJump, Properties.Resources.bumarangflower };
            if (ItemName.Length != ItemPicture.Length)
            {
                new Exception("Item Name und Picture müssen gleich viele Elemente enthalten");
            }
            random = new Random();
            Size = new Size(Settings.width, Settings.height);
            Tag = "";
            active = true;
        }
        public PictureBox Activate(bool mushroom)
        {
            if (!active) return null;
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
            return new PictureBox()
            {
                Tag = "Item_" + randomNr,
                Image = ItemPicture[randomNr],
                AccessibleDescription = ItemName[randomNr],
                Size = Size,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(Location.X, Location.Y - Settings.height)
            };
            //Set Picture off
        }
    }
}
