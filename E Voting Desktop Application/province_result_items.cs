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
    public partial class province_result_items : Form
    {
        public province_result_items()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {   
            result_items ass = new result_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            result_sindh ass = new result_sindh();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            result_punjab ass = new result_punjab();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            result_baluchistan ass = new result_baluchistan();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            result_kpk ass = new result_kpk();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
