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
    public partial class regs_items : Form
    {
        public regs_items()
        {
            InitializeComponent();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            registeration r = new registeration();
            this.Hide();
            r.ShowDialog();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            pollingStation_regs ps = new pollingStation_regs();
            this.Hide();
            ps.ShowDialog();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            CandidateRegs cr = new CandidateRegs();
            this.Hide();
            cr.ShowDialog();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard ds = new dashboard();
            this.Hide();
            ds.ShowDialog();
        }
    }
}
