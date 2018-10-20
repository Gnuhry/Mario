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
    public partial class Play : Form
    {
        private Engine engine;
        public Play(int Level,Settings settings)
        {
            InitializeComponent();
            engine = new Engine(new ReadFile(Level).InterpretFile(settings), Controls);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
    }
}
