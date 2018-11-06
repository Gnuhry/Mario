using System;
using System.Windows.Forms;

namespace Mario
{
    public partial class pause : Form
    {
        private Play play;
        private Settings settings;
        private Worlds worlds;
        public pause(Play play, bool[] star)
        {
            InitializeComponent();
            this.play = play;
            play.Enabled = false;
            settings = play.GetSettings();
            worlds = play.GetWorlds();
            InitStar(star);
        }

        private void InitStar(bool[] star)
        {
            if (star[0])
            {
                pcBStar1.Image = Properties.Resources.star;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.star;
            }
            if (star[1])
            {
                pcBStar1.Image = Properties.Resources.star;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.star;
            }
            if (star[2])
            {
                pcBStar1.Image = Properties.Resources.star;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.star;
            }//TODO
        }

        private void btnMenue_Click(object sender, EventArgs e)
        {
            play.Close();
            worlds.Visible = true;
            worlds.ShowInTaskbar = true;
            Close();
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
            play.Enabled = true;
            Close();
        }
    }
}
