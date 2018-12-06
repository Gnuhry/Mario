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
            chBmusic.Checked = settings.Music;
            chBsound.Checked = settings.Sounds;
            tBarMusicValue.Value = Convert.ToInt32(100 * settings.Volume);
            if (chBmusic.Checked)
            {
                tBarMusicValue.Enabled = true;
            }
            else
            {
                tBarMusicValue.Enabled = false;
            }
        }
        private void SetLabel()
        {
            label2.ForeColor = label4.ForeColor = label6.ForeColor = label8.ForeColor = Color.Black;
            label2.Text = settings.Up + "";
            label4.Text = settings.Right + "";
            label6.Text = settings.Left + "";
            label8.Text = settings.Item + "";

            if (settings.Up.Equals(settings.Right))
            {
                label2.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
            }
            if (settings.Up.Equals(settings.Left))
            {
                label2.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
            }
            if (settings.Up.Equals(settings.Item))
            {
                label2.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
            }
            if (settings.Right.Equals(settings.Left))
            {
                label4.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
            }
            if (settings.Right.Equals(settings.Item))
            {
                label4.ForeColor = Color.Red;
                label8.ForeColor = Color.Red;
            }
            if (settings.Left.Equals(settings.Item))
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
                case 0: btnJump.Enabled = true; settings.Up = keys; break;
                case 1: btnRight.Enabled = true; settings.Right = keys; break;
                case 2: btnLeft.Enabled = true; settings.Left = keys; break;
                case 3: btnItem.Enabled = true; settings.Item = keys; break;
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
            settings.Music = !settings.Music;
            chBmusic.Checked = settings.Music;
            Sound_music.CheckMusic(settings);
            if (chBmusic.Checked)
            {
                tBarMusicValue.Enabled = true;
            }
            else
            {
                tBarMusicValue.Enabled = false;
            }
        }

        private void chBsound_Click(object sender, EventArgs e)
        {
            settings.Sounds = !settings.Sounds;
            chBsound.Checked = settings.Sounds;
        }

        private void btnResetUp_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Reset");
            settings.Up = Settings.upD;
            SetLabel();
        }

        private void btnResetRight_Click(object sender, EventArgs e)
        {
            settings.Right = Settings.rightD;
            SetLabel();
        }

        private void btnResetLeft_Click(object sender, EventArgs e)
        {
            settings.Left = Settings.leftD;
            SetLabel();
        }

        private void btnResetItem_Click(object sender, EventArgs e)
        {
            settings.Item = Settings.itemD;

            SetLabel();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            settings.Default();
            SetLabel();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            ReadFile.ResetAll(settings);
            Application.Restart();
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReadFile.SetSettings(settings);
        }

        private void tBarMusicValue_Scroll(object sender, EventArgs e)
        {
            settings.Volume = tBarMusicValue.Value / 100.0;
            Sound_music.ChangeVolume(settings);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
