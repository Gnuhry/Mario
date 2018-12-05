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
            if (highscore.Equals("-1"))
            {
                lbHigscore.Text = "none";
            }
            else
            {
                lbHigscore.Text = highscore;
            }
            string[] stars = star.Split(',');
            if (stars[0].Equals("1"))
            {
                pcBStar1.Image = Properties.Resources.ricecoin;
            }
            else
            {
                pcBStar1.Image = Properties.Resources.ricecoin_not;
            }
            if (stars[1].Equals("1"))
            {
                pcBStar2.Image = Properties.Resources.ricecoin;
            }
            else
            {
                pcBStar2.Image = Properties.Resources.ricecoin_not;
            }
            if (stars[2].Equals("1"))
            {
                pcBStar3.Image = Properties.Resources.ricecoin;
            }
            else
            {
                pcBStar3.Image = Properties.Resources.ricecoin_not;
            }
        }
    }
}
