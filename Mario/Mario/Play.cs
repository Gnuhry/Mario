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
    public partial class Play : Form
    {
        private Engine engine;
        private Settings settings;
        private int level;
        private Form1 menue;
        public Play(int level, Settings settings, Form1 menue)
        {
            InitializeComponent();
            this.level = level;
            this.menue = menue;
            engine = new Engine(new ReadFile(level).InterpretFile(settings), Controls);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.settings = settings;
        }
        public Settings GetSettings() => settings;
        public Form1 GetMenue() => menue;
        public void Restart()
        {
            Label label = label1;
            Play play = new Play(level, settings, menue);
            Dispose();
            play.Show();
        }
    }
}
