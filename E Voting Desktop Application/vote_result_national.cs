using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Voting_Desktop_Application
{
    public partial class vote_result_national : Form
    {
        public vote_result_national()
        {
            InitializeComponent();
        }
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            result_items ass = new result_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void vote_result_national_Load(object sender, EventArgs e)
        {
            String voteCount="", candidateName = "", PartyName = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetTopNationalAssemblyCandidate]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    voteCount = dt.Rows[i]["voteCount"].ToString();
                    candidateName = dt.Rows[i]["candidateName"].ToString();
                    PartyName = dt.Rows[i]["party"].ToString();
                    MessageBox.Show(dt.Rows[i]["voteCount"].ToString());
                }
                String partyName2=dt.Rows[1]["party"].ToString();
                String partyName3 = dt.Rows[0]["party"].ToString();
                label9.Text = PartyName;
                label10.Text = candidateName;
                label11.Text = voteCount;
                if (PartyName == "PTI")
                {
                    pictureBox1.Image = Properties.Resources.ptiFlag;
                }
                else if (PartyName == "ANP")
                {
                    pictureBox1.Image = Properties.Resources.anpFlag;
                }
                else if (PartyName == "PML(N)")
                {
                    pictureBox1.Image = Properties.Resources.pmlnFlag;
                }
                else if (PartyName == "MQM")
                {
                    pictureBox1.Image = Properties.Resources.mqmFlag;
                }
                else if (PartyName == "PPP")
                {
                    pictureBox1.Image = Properties.Resources.pppFlag;
                }

                //Second 
                if (partyName2 == "PTI")
                {
                    pictureBox2.Image = Properties.Resources.ptiFlag;
                }
                else if (partyName2 == "ANP")
                {
                    pictureBox2.Image = Properties.Resources.anpFlag;
                }
                else if (partyName2 == "PML(N)")
                {
                    pictureBox2.Image = Properties.Resources.pmlnFlag;
                }
                else if (partyName2 == "MQM")
                {
                    pictureBox2.Image = Properties.Resources.mqmFlag;
                }
                else if (partyName2 == "PPP")
                {
                    pictureBox2.Image = Properties.Resources.pppFlag;
                }
                //Third

                 
                if (partyName3 == "PTI")
                {
                    pictureBox3.Image = Properties.Resources.ptiFlag;
                }
                else if (partyName3 == "ANP")
                {
                    pictureBox3.Image = Properties.Resources.anpFlag;
                }
                else if (partyName3 == "PML(N)")
                {
                    pictureBox3.Image = Properties.Resources.pmlnFlag;
                }
                else if (partyName3 == "MQM")
                {
                    pictureBox3.Image = Properties.Resources.mqmFlag;
                }
                else if (partyName3 == "PPP")
                {
                    pictureBox3.Image = Properties.Resources.pppFlag;
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    }
}
