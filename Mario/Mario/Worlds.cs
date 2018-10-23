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
    public partial class Worlds : Form
    {
        private int level;
        private int levelMax = 4;
        private Settings setting;
        private Form1 menue;
        public Worlds(Settings settings, Form1 form1)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            level = 1;
            InitializeComponent();
            setting = settings;
            menue = form1;
            MoveToLevel();
        }

        private void Worlds_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
            {
                if (level == 1)
                {
                    return;
                }
                level--;
                MoveToLevel();
            }
            if (e.KeyData == Keys.Right)
            {
                if (level == levelMax)
                {
                    return;
                }
                level++;
                MoveToLevel();
            }
            if (e.KeyData == Keys.Enter||e.KeyData==Keys.Space)
            {
                Visible = false;
                ShowInTaskbar = false;
                Play play = new Play(level, setting,this);
                play.Show();
            }
            if (e.KeyData == Keys.Escape)
            {
                menue.Visible = true;
                menue.ShowInTaskbar = true;
                Close();
            }
        }
        private void MoveToLevel()
        {
            PictureBox pcB = new PictureBox();
            switch (level)
            {
                case 1: pcB = pcB1; break;
                case 2: pcB = pcB2; break;
                case 3: pcB = pcB3; break;
                case 4: pcB = pcB4; break;
            }
            Point help = player.Location;
            help.Offset(new Point(pcB.Location.X - help.X, pcB.Location.Y - help.Y));
            player.Location = help;
            player.BringToFront();
        }
    }
}
