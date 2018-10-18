using System;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        private Engine engine;
        private Settings settings;
        public Form1()
        {
            InitializeComponent();
            settings = new Settings();
            engine = new Engine(new ReadFile(1).InterpretFile(settings), Controls);
        }

    }
}
