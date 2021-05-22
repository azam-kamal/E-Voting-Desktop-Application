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
    public partial class vote_cast : Form
    {

        public static String nAParty = "";
        public static String nACandidate = "";
        public vote_cast()
        {
            InitializeComponent();
        }
        String getPollingStationNumer = voting_place.pollingStation;
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            nACandidate = label6.Text;
            nAParty = partyDropDown.selectedValue.ToString();
            vote_cast2 ass = new vote_cast2();
            this.Hide();
            ass.ShowDialog();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "admin")
            {
                voting_items ass = new voting_items();
                this.Hide();
                ass.ShowDialog();
            }
        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(voting_place.id, ToString());
                MessageBox.Show(getPollingStationNumer);
                MessageBox.Show(partyDropDown.selectedValue.ToString());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetNationalAssemblyCandidates]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@pollingStationId", Convert.ToInt32(voting_place.id));
                da.SelectCommand.Parameters.AddWithValue("@pollingStationNumber", Convert.ToString(getPollingStationNumer));
                da.SelectCommand.Parameters.AddWithValue("@representation", "National Assembly");
                da.SelectCommand.Parameters.AddWithValue("@party", partyDropDown.selectedValue.ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MessageBox.Show(dt.Rows[i]["candidate_name"].ToString());
                    label6.Text = dt.Rows[i]["candidate_name"].ToString();
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
        }
    }
}
