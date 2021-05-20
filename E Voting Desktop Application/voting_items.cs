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
            groupBox1.Visible = true;
            
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard ass = new dashboard();
            this.Hide();
            ass.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "admin")
            {
                voting_place ass = new voting_place();
                this.Hide();
                ass.ShowDialog();
            }
            else
            {
                MessageBox.Show("password was incorrect");
            }
        }
    }
}
