using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class score : Form
    {
        private int counter, coins;
        private double timecounter, time, higscoore;
        private bool[] ricecoins;
        private Worlds worlds;
        private bool realoud;
        private Timer timer;
        public score(double time, double higscoore, int coins, bool[] ricecoins, Worlds worlds, bool[] formerriceCoin)
        {
            realoud = false;
            this.worlds = worlds;
            this.ricecoins = ricecoins;
            this.time = time;
            this.coins = coins;
            this.higscoore = higscoore;
            timecounter = 1;
            counter = 0;
            InitializeComponent();
            if (formerriceCoin[0])
            {
                pcBrCoin1.Image = Properties.Resources.ricecoin;
            }
            if (formerriceCoin[1])
            {
                pcBrCoin2.Image = Properties.Resources.ricecoin;
            }
            if (formerriceCoin[2])
            {
                pcBrCoin3.Image = Properties.Resources.ricecoin;
            }
            panel1.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - (panel1.Width / 2), Screen.PrimaryScreen.WorkingArea.Height / 2 - (panel1.Height / 2));
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1;
            timer.Start();
        }
        private void score_KeyDown(object sender, KeyEventArgs e)
        {
            if (realoud) { return; }
            if (e.KeyData == Keys.Space)
            {
                switch (counter)
                {
                    case 0:
                        Sound_music.CounterSound(worlds.GetSetting());
                        lbTime.Text = time + " sek";
                        if (time > higscoore)
                        {
                            lbNewRecord.Visible = true;
                        }
                        counter++;
                        break;
                    case 1:
                    case 2:
                        Sound_music.CounterSound(worlds.GetSetting());
                        lbCoin.Text = coins + " Coins";
                        counter++;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        if (ricecoins[0])
                        {
                            Sound_music.RiceCoinSound(worlds.GetSetting());
                            pcBrCoin1.Image = Properties.Resources.ricecoin;
                        }
                        if (ricecoins[1])
                        {
                            Sound_music.RiceCoinSound(worlds.GetSetting());
                            pcBrCoin2.Image = Properties.Resources.ricecoin;
                        }
                        if (ricecoins[2])
                        {
                            Sound_music.RiceCoinSound(worlds.GetSetting());
                            pcBrCoin3.Image = Properties.Resources.ricecoin;
                        }
                        break;
                    default: ToWorld(); break;
                }
            }
            else if (e.KeyData == Keys.Escape)
            {
                ToWorld();
            }
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            if (lbNewRecord.Visible && counter % 5 == 0)
            {
                if (lbNewRecord.ForeColor == Color.Yellow)
                {
                    lbNewRecord.ForeColor = Color.OrangeRed;
                }
                else
                {
                    lbNewRecord.ForeColor = Color.Yellow;
                }
            }
            switch (counter)
            {
                case 0:
                    if (timecounter < time)
                    {
                        Sound_music.CounterSound(worlds.GetSetting());
                        lbTime.Text = timecounter + " sek";
                        timecounter += 1;
                        break;
                    }
                    lbTime.Text = time + " sek";
                    if (time < higscoore)
                    {
                        lbNewRecord.Visible = true;
                    }
                    counter++;
                    break;
                case 1:
                    timecounter = 1;
                    counter++;
                    break;
                case 2:
                    if (timecounter < coins)
                    {
                        Sound_music.CounterSound(worlds.GetSetting());
                        lbCoin.Text = timecounter + " Coins";
                        timecounter++;
                        break;
                    }
                    counter++;
                    break;
                case 3:
                    if (ricecoins[0])
                    {
                        Sound_music.RiceCoinSound(worlds.GetSetting());
                        pcBrCoin1.Image = Properties.Resources.ricecoin;
                    }
                    counter++;
                    break;
                case 10:
                    if (ricecoins[1])
                    {
                        Sound_music.RiceCoinSound(worlds.GetSetting());
                        pcBrCoin2.Image = Properties.Resources.ricecoin;
                    }
                    counter++;
                    break;
                case 17:
                    if (ricecoins[2])
                    {
                        Sound_music.RiceCoinSound(worlds.GetSetting());
                        pcBrCoin3.Image = Properties.Resources.ricecoin;
                    }
                    counter++;
                    break;
                case 400: ToWorld(); break;
                default: counter++; break;
            }
        }
        private void ToWorld()
        {
            if (realoud)
            {
                Close();
            }
            timer.Stop();
            worlds.Reload();
            realoud = true;
            Close();
        }
    }
}
