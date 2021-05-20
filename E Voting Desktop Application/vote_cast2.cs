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
        public static string pACandidate="";
        public static string pAParty = "";

        public vote_cast2()
        {
            InitializeComponent();
        }

       
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            pAParty = bunifuDropdown1.selectedValue.ToString();
            pACandidate = label6.Text;
            voter_cast3 ass = new voter_cast3();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
