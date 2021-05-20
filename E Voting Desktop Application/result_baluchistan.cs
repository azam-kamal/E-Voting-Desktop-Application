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
    public partial class result_baluchistan : Form
    {
        public result_baluchistan()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            province_result_items ass = new province_result_items();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
