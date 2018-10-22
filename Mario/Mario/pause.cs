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
    public partial class pause : Form
    {
        private Play play;
        private Settings settings;
        private Worlds worlds;
        public pause(Play play, Settings settings, Worlds worlds)
        {
            InitializeComponent();
            this.play = play;
            this.settings = settings;
            this.worlds = worlds;
            btnGoOn.Focus();
        }

        private void btnMenue_Click(object sender, EventArgs e)
        {
            play.Close();
            worlds.Visible = true;
            worlds.ShowInTaskbar = true;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Setting setting = new Setting(settings);
            setting.Show();
            setting.FormClosed += Setting_FormClosed;
        }

        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Enabled = true;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            play.Restart();
            Close();
        }

        private void btnGoOn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
