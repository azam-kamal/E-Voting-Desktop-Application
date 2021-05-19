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
    public partial class voting_items : Form
    {
        public voting_items()
        {
            InitializeComponent();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            voting_place ass = new voting_place();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
