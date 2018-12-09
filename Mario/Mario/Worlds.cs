using System;
using System.Drawing;
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
            Console.WriteLine("Reloud");
            try
            {
                Visible = true;
                ShowInTaskbar = true;
                SetText("1-1");
                SetText("1-2");
                SetText("1-3");
                SetText("1-4");
                LoadActivated();
                MoveToLevel();
            }
            catch (Exception)
            {

            }
        }

        private void LoadActivated()
        {
            pcB1.Image = pcB2.Image = pcB3.Image = pcB4.Image = null;
            pcB1.Tag = new ReadFile(world + "-1").SearchData().Split('|')[4];
            pcB2.Tag = new ReadFile(world + "-2").SearchData().Split('|')[4];
            pcB3.Tag = new ReadFile(world + "-3").SearchData().Split('|')[4];
            pcB4.Tag = new ReadFile(world + "-4").SearchData().Split('|')[4];
            if (!pcB2.Tag.ToString().Equals("1"))
            {
                pcB2.Image = Properties.Resources.lock1;
            }
            if (!pcB3.Tag.ToString().Equals("1"))
            {
                pcB3.Image = Properties.Resources.lock1;
            }
            if (!pcB4.Tag.ToString().Equals("1"))
            {
                pcB4.Image = Properties.Resources.lock1;
            }
        }

        private void Worlds_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToChar(e.KeyValue).Equals(setting.Left))
            {
                if (level == 1)
                {
                    return;
                }
                level--;
                MoveToLevel();
            }
            if (Convert.ToChar(e.KeyValue).Equals(setting.Right))
            {
                if (level == levelMax)
                {
                    return;
                }
                switch (level)
                {
                    case 1: if (pcB2.Image != null) { return; } break;
                    case 2: if (pcB3.Image != null) { return; } break;
                    case 3: if (pcB4.Image != null) { return; } break;
                }
                level++;
                MoveToLevel();
            }
            if (e.KeyData == Keys.Enter || Convert.ToChar(e.KeyValue).Equals(setting.Item))
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
        public Settings GetSetting() => setting;
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
                                if (Convert.ToInt32(help2[1]) == 3)
                                {
                                    int ricecoinamount = 0;
                                    string[] data = new ReadFile("1-1").SearchData().Split('|')[3].Split(',');
                                    foreach (string s in data)
                                    {
                                        if (s.Equals("1"))
                                        {
                                            ricecoinamount++;
                                        }
                                    }
                                    data = new ReadFile("1-2").SearchData().Split('|')[3].Split(',');
                                    foreach (string s in data)
                                    {
                                        if (s.Equals("1"))
                                        {
                                            ricecoinamount++;
                                        }
                                    }
                                    data = help[3].Split(',');
                                    foreach (string s in data)
                                    {
                                        if (s.Equals("1"))
                                        {
                                            ricecoinamount++;
                                        }
                                    }
                                    if (ricecoinamount > 8)
                                    {
                                        ReadFile.UnlockLevel(help2[0] + "-" + (Convert.ToInt32(help2[1]) + 1));
                                    }
                                }
                                else
                                {
                                    ReadFile.UnlockLevel(help2[0] + "-" + (Convert.ToInt32(help2[1]) + 1));
                                }
                                Controls[f].Text = level + " " + help[0].Split('#')[1] + " | " + help[1] + "sek";
                            }
                            else
                            {
                                Controls[f].Text = level + " " + help[0].Split('#')[1] + " | no record";
                            }
                        }
                        else
                        {
                            Controls[f].Text = "";
                        }
                        return;
                    }
                }
            }
        }
    }
}
