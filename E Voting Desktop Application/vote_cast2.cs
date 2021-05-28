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
    public partial class vote_cast2 : Form
    {
        public static string pACandidate="";
        public static string pAParty = "";

        public vote_cast2()
        {
            InitializeComponent();
        }
        String getPollingStationNumer = voting_place.pollingStation;
        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            pAParty = partyDropDown.selectedValue.ToString();
            pACandidate = label6.Text;
            voter_cast3 ass = new voter_cast3();
            this.Hide();
            ass.ShowDialog();
        }

        private void party_onItemSelected(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(voting_place.id, ToString());
                MessageBox.Show(getPollingStationNumer);
                MessageBox.Show(partyDropDown.selectedValue.ToString());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetProvincialAssemblyCandidate]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@pollingStationId", Convert.ToInt32(voting_place.id));
                da.SelectCommand.Parameters.AddWithValue("@pollingStationNumber", Convert.ToString(getPollingStationNumer));
                da.SelectCommand.Parameters.AddWithValue("@representation", "Provincial Assembly");
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
