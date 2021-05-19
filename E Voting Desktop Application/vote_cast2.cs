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
    public partial class vote_cast2 : Form
    {
        private string v1;
        private string v2;

        public vote_cast2()
        {
            InitializeComponent();
        }

        public vote_cast2(string v1, string v2, string text)
        {
            this.v1 = v1;
            this.v2 = v2;
            Text = text;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            voter_cast3 ass = new voter_cast3(v1,v2,Text,bunifuDropdown1.selectedValue.ToString(),label6.Text);
            this.Hide();
            ass.ShowDialog();
        }
    }
}
