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

        public static String nAParty = "";
        public static String nACandidate = "";
        public vote_cast()
        {
            InitializeComponent();
        }


        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            nACandidate = label6.Text;
            nAParty = bunifuDropdown1.selectedValue.ToString();
            vote_cast2 ass = new vote_cast2();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
