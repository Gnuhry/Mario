using System;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        private Player player;
        private Engine engine;
        private Settings settings;
        private Items items;
        public Form1()
        {
            InitializeComponent();
            settings = new Settings();
            items = new Items();
            player = new Player(label1, items);
            engine = new Engine(new ReadFile(1).InterpretFile(), player, items, Controls);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToChar(e.KeyValue) == settings.up)
            {
                player.jump = true;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.left)
            {
                player.left = true;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.right)
            {
                player.right = true;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.item)
            {
                items.UseItem(player.control.Location);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToChar(e.KeyValue) == settings.up)
            {
                player.jump = false;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.left)
            {
                player.left = false;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.right)
            {
                player.right = false;
            }
            else if (Convert.ToChar(e.KeyValue) == settings.item)
            {

            }
        }
    }
}
