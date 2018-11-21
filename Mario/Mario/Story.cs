using System.Windows.Forms;

namespace Mario
{
    public partial class Story : Form
    {
        private int click;
        private void Story_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
            click++;
            if (click == 1)
            {
                pictureBox2.Visible = true;
            }
            else if (click == 2)
            {
                pictureBox3.Visible = true;
                pictureBox1.Visible = false;
                pictureBox1.Image = Properties.Resources.story_4;
            }
            else if (click == 3)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                pictureBox2.Image = Properties.Resources.story_5;
            }
            else if (click == 4)
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox3.Image = Properties.Resources.dirt;
            }
            else if (click == 5)
            {
                pictureBox3.Visible = true;
            }
            else
            {
                Close();
            }
        }

        public Story()
        {
            InitializeComponent();
            click = 0;
        }
    }
}
