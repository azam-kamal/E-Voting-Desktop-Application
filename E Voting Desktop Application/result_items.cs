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
    public partial class result_items : Form
    {
        public result_items()
        {
            InitializeComponent();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            //National Result
            vote_result_national ass = new vote_result_national();
            this.Hide();
            ass.ShowDialog();

        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            province_result_items ass = new province_result_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard ass = new dashboard();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuSeparator1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
