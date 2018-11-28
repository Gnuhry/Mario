using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Play : Form
    {
        private Engine engine;
        private Settings settings;
        private string level;
        private Worlds worlds;
        private ReadFile readFile;
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public Play(string level, Settings settings, Worlds worlds)
        {
            allDone.Reset();
            InitializeComponent();
            Visible = false;
            readFile = new ReadFile(level);
            string[] data = readFile.SearchData().Split('|');
            LoadingLevel loadingScreen = new LoadingLevel("Level " + level, data[0].Split('#')[1], data[1], data[3]);
            loadingScreen.Show();
            new Thread(LoadingScreen).Start();

            this.level = level;
            this.worlds = worlds;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.settings = settings;
            engine = new Engine(readFile.InterpretFile(settings), Controls);

            allDone.WaitOne();
            Visible = true;
            loadingScreen.Close();

            Focus();
            engine.Start();
        }
        private void LoadingScreen()
        {
            Thread.Sleep(Settings.loadingScreenLength);
            allDone.Set();
        }
        public Settings GetSettings() => settings;
        public Worlds GetWorlds() => worlds;
        public Engine GetEngine() => engine;
        public void Restart()
        {
            Label label = label1;
            Play play = new Play(level, settings, worlds);
            engine.PlayerDipose();
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
                engine.StartTime();
            }
        }
        //---------------------------------Highscore-----------------------------------
        private void SaveHighscoore(double time)
        {
            string data = readFile.GetData();
            string[] help = data.Split('|');
            help[1] = time.ToString();
            help[4] = "1";
            string erg = "";
            for (int f = 0; f < help.Length-1; f++)
            {
                erg += help[f] + "|";
            }
            Console.WriteLine(erg);
            readFile.SetData(erg);
        }
        private double GetHigscoore()
        {
            string data = readFile.GetData();
            string time = data.Split('|')[1];
            if (time.Equals("-1"))
            {
                return Double.MaxValue;
            }
            return Convert.ToDouble(time);
        }
        public void CheckHighScoore()
        {
            engine.StopTime();
            Console.WriteLine(GetHigscoore() + ">" + engine.GetTime());
            if (GetHigscoore() > engine.GetTime())
            {
                //New Highscore
                Console.WriteLine("Yeah");
                SaveHighscoore(engine.GetTime());
            }
        }
        public void SetRiceCoin(bool[] riceCoin)
        {
            string txt = "";
            foreach (bool rice in riceCoin)
            {
                if (rice)
                {
                    txt += "1,";
                }
                else
                {
                    txt += "0,";
                }
            }
            txt=txt.Substring(0, txt.Length - 1);
            string data = readFile.SearchData();
            string[] help = data.Split('|');
            help[3] = txt;
            string erg = "";
            for (int f = 0; f < help.Length-1; f++)
            {
                erg += help[f] + "|";
            }
            readFile.SetData(erg);
        }
    }
}
