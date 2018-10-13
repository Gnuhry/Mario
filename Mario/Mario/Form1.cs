using System;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        private Player player;
        private Engine engine;
        public Form1()
        {
            InitializeComponent();
            player = new Player(label1);
            engine = new Engine(new ReadFile(1).interpretFile(), player);
            
            Items items = new Items();
            player = new Player(label1, items);
            engine = new Engine(new ReadFile(1).interpretFile(), player, items);
            engine.DisplayBackground(Controls);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(e.KeyValue));
            switch (Convert.ToChar(e.KeyValue))
            {
                case 'W': player.jump = true; break;
                case 'A': player.left = true; break;
                case 'D': player.right = true; break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(e.KeyValue));
            switch (Convert.ToChar(e.KeyValue))
            {
                case 'W': player.jump = false; break;
                case 'A': player.left = false; break;
                case 'D': player.right = false; break;
            }
        }
    }
}
