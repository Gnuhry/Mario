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
        private Worlds worlds;
        public Play(int level, Settings settings, Worlds worlds)
        {
            InitializeComponent();
            this.level = level;
            this.worlds = worlds;
            engine = new Engine(new ReadFile(level).InterpretFile(settings), Controls);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.settings = settings;
        }
        public Settings GetSettings() => settings;
        public Worlds GetWorlds() => worlds;
        public void Restart()
        {
            Label label = label1;
            Play play = new Play(level, settings, worlds);
            Dispose();
            play.Show();
        }
        public void SetCoin(int coin)
        {
            lbCoins.Text = coin + "";
        }

        private void Play_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
            {
                engine.PlayerStart();
            }
        }
    }
}
