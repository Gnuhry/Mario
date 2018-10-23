using System;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        private Settings settings;
        private Setting setting;
        public Form1()
        {
            InitializeComponent();
            settings = new Settings();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            new Worlds(settings,this).Show();
          
        }

        private void Forms_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true; 
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            setting = new Setting(settings);
            setting.Show();
            setting.FormClosed += Forms_FormClosed;
        }
    }
}
