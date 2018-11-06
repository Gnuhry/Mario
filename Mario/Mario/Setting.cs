using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class Setting : Form
    {
        private Settings settings;
        private char keys;
        private int mode;
        public Setting(Settings settings)
        {
            InitializeComponent();
            Focus();
            this.settings = settings;
            SetLabel();
            chBmusic.Checked = settings.music;
            chBsound.Checked = settings.sounds;
        }
        private void SetLabel()
        {
            label2.ForeColor = label4.ForeColor = label6.ForeColor = label8.ForeColor = Color.Black;
            label2.Text = settings.up + "";
            label4.Text = settings.right + "";
            label6.Text = settings.left + "";
            label8.Text = settings.item + "";

            if (settings.up.Equals(settings.right))
            {
                label2.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
            }
            if (settings.up.Equals(settings.left))
            {
                label2.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
            }
            if (settings.up.Equals(settings.item))
            {
                label2.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
            }
            if (settings.right.Equals(settings.left))
            {
                label4.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
            }
            if (settings.right.Equals(settings.item))
            {
                label4.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
            }
            if (settings.left.Equals(settings.item))
            {
                label6.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
            }
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            mode = 0;
            btnJump.KeyDown += Setting_KeyDown;
            label2.Text = "?";
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            mode = 1;
            btnRight.KeyDown += Setting_KeyDown;
            label4.Text = "?";
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            mode = 2;
            btnLeft.KeyDown += Setting_KeyDown;
            label6.Text = "?";
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            mode = 3;
            btnItem.KeyDown += Setting_KeyDown;
            label8.Text = "?";
        }
        private void StopKeyRecord()
        {
            KeyDown -= Setting_KeyDown;
            if (keys == '_')
            {
                return;
            }
            switch (mode)
            {
                case 0: btnJump.Enabled = true; settings.up = keys; break;
                case 1: btnRight.Enabled = true; settings.right = keys; break;
                case 2: btnLeft.Enabled = true; settings.left = keys; break;
                case 3: btnItem.Enabled = true; settings.item = keys; break;
            }
            SetLabel();
        }

        private void Setting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                keys = '_';
                StopKeyRecord();
            }
            else if ((e.KeyValue >= Convert.ToInt32('0') && e.KeyValue <= Convert.ToInt32('9')) || (e.KeyValue >= Convert.ToInt32('A') && e.KeyValue <= Convert.ToInt32('Z')))
            {
                keys = Convert.ToChar(e.KeyValue);
                StopKeyRecord();
            }
        }

        private void chBmusic_Click(object sender, EventArgs e)
        {
            settings.music = !settings.music;
            chBmusic.Checked = settings.music;
            if (settings.music)
            {
                sound_music.StartMusic(settings);
            }
            else
            {
                sound_music.StopMusic();
            }
        }

        private void chBsound_Click(object sender, EventArgs e)
        {
            settings.sounds = !settings.sounds;
            chBsound.Checked = settings.sounds;
        }

        private void btnResetUp_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Reset");
            settings.up = Settings.upD;
            SetLabel();
        }

        private void btnResetRight_Click(object sender, EventArgs e)
        {
            settings.right = Settings.rightD;
            SetLabel();
        }

        private void btnResetLeft_Click(object sender, EventArgs e)
        {
            settings.left = Settings.leftD;
            SetLabel();
        }

        private void btnResetItem_Click(object sender, EventArgs e)
        {
            settings.item = Settings.itemD;

            SetLabel();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            settings.Default();
            SetLabel();
        }
    }
}
