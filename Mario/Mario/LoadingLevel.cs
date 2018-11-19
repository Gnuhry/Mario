using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class LoadingLevel : Form
    {
        public LoadingLevel(string level, string name, string highscore, string star)
        {
            InitializeComponent();
            panel.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - (panel.Width / 2), Screen.PrimaryScreen.WorkingArea.Height / 2 - (panel.Height / 2));
            lbLevel.Text = level;
            lbLevelName.Text = name;
            lbHigscore.Text = highscore;
            string[] stars = star.Split(',');
            if (stars[0].Equals("1"))
            {
                pcBStar1.Image = Properties.Resources.stone;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.stone;
            }
            if (stars[1].Equals("1"))
            {
                pcBStar1.Image = Properties.Resources.stone;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.stone;
            }
            if (stars[2].Equals("1"))
            {
                pcBStar1.Image = Properties.Resources.stone;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.stone;
            }//TODO
        }
    }
}
