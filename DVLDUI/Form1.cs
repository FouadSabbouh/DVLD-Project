using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayar;

namespace DVLDUI
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form PeopleMange = new mangePeoplefrm();
            PeopleMange.ShowDialog();
        }
    }
}
