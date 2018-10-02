using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        private Spieler spieler;
        public Form1()
        {
            InitializeComponent();
            spieler=new Spieler(label1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(e.KeyValue));
            switch (Convert.ToChar(e.KeyValue)) {
                case 'W': spieler.Jump(true); break;
                case 'A': spieler.Bewegung(false,true); break;
                case 'D': spieler.Bewegung(true,false); break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(e.KeyValue));
            switch (Convert.ToChar(e.KeyValue))
            {
                case 'W': spieler.Jump(false); break;
                case 'A': spieler.Bewegung(false, false); break;
                case 'D': spieler.Bewegung(false, false); break;
            }
        }
    }
}
