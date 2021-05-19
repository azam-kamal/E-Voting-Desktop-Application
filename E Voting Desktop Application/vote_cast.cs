using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Voting_Desktop_Application
{
    public partial class vote_cast : Form
    {
        private string v;

        public vote_cast()
        {
            InitializeComponent();
        }

        public vote_cast(string v)
        {
            this.v = v;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            vote_cast2 ass = new vote_cast2(v,bunifuDropdown1.selectedValue.ToString(),label6.Text);
            this.Hide();
            ass.ShowDialog();
        }
    }
}
