﻿using System;
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
        private int level, world;
        private int levelMax = 4;
        private Settings setting;
        private Form1 menue;
        private bool activated;
        public Worlds(Settings settings, Form1 form1)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            level = world = 1;
            InitializeComponent();
            SetText("1-1");
            SetText("1-2");
            SetText("1-3");
            SetText("1-4");
            LoadActivated();          
            setting = settings;
            menue = form1;
            MoveToLevel();
        }
        public void Reload()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            SetText("1-1");
            SetText("1-2");
            SetText("1-3");
            SetText("1-4");
            LoadActivated();
            MoveToLevel();
        }

        private void LoadActivated()
        {
            pcB1.Tag = new ReadFile(world + "-1").SearchData().Split('|')[4];
            pcB2.Tag = new ReadFile(world + "-2").SearchData().Split('|')[4];
            pcB3.Tag = new ReadFile(world + "-3").SearchData().Split('|')[4];
            pcB4.Tag = new ReadFile(world + "-4").SearchData().Split('|')[4];

        }

        private void Worlds_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToChar(e.KeyValue).Equals(setting.left))
            {
                if (level == 1)
                {
                    return;
                }
                level--;
                MoveToLevel();
            }
            if (Convert.ToChar(e.KeyValue).Equals(setting.right))
            {
                if (level == levelMax)
                {
                    return;
                }
                level++;
                MoveToLevel();
            }
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Space)
            {
                if (activated)
                {
                    Visible = false;
                    ShowInTaskbar = false;
                    new Play(world + "-" + level, setting, this).Show();
                }
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
            activated = pcB.Tag.Equals("1");
        }

        private void SetText(string level)
        {
            for (int f = 0; f < Controls.Count; f++)
            {
                if (Controls[f].Tag != null)
                {
                    if (Controls[f].Tag.ToString().Equals(level))
                    {
                        string[] help = new ReadFile(level).SearchData().Split('|');
                        if (help[4].Equals("1"))
                        {
                            if (!help[1].Equals("-1"))
                            {
                                string[] help2 = level.Split('-');
                                ReadFile.UnlockLevel(help2[0] + "-" + (Convert.ToInt32(help2[1]) + 1));
                                Controls[f].Text = level + " " + help[0].Split('#')[1] + " | " + help[1] + "sek";
                            }
                            else
                            {
                                Controls[f].Text = level + " " + help[0].Split('#')[1] + " | no record";
                            }
                        }
                        else
                        {
                            Controls[f].Text = "?!?!?!?!?!?!?!?!?!?!?!?!";
                        }
                        return;
                    }
                }
            }
        }
    }
}
