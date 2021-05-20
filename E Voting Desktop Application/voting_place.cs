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
    
    public partial class voting_place : Form
    {
        public static String pollingStation = "";
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        String id;
        int btnClick = 0;
        public voting_place()
        {
            InitializeComponent();
        }

        private void provinceDropdown1_onItemSelected(object sender, EventArgs e)
        {
            if (provinceDropdown1.selectedValue == "Sindh")
            {
                cityDropdown2.AddItem("Karachi");
                cityDropdown2.AddItem("Hyderabad");
                cityDropdown2.AddItem("Larkana");
                cityDropdown2.AddItem("Sukkur");
            }
            else if (provinceDropdown1.selectedValue == "Baluchistan")
            {
                cityDropdown2.AddItem("Quetta");
                cityDropdown2.AddItem("Ziarat");
                cityDropdown2.AddItem("Chaman");
                cityDropdown2.AddItem("Sui");
            }
            else if (provinceDropdown1.selectedValue == "Punjab")
            {
                cityDropdown2.AddItem("Lahore");
                cityDropdown2.AddItem("Multan");
                cityDropdown2.AddItem("Faisalabad");
                cityDropdown2.AddItem("Bhawalpur");
            }
            else if (provinceDropdown1.selectedValue == "Khyber Pakhtunkhwa")
            {
                cityDropdown2.AddItem("Peshawar");
                cityDropdown2.AddItem("Mardan");
                cityDropdown2.AddItem("Swat");
                cityDropdown2.AddItem("Abbottabad");
            }
        }

        private void cityDropdown2_onItemSelected(object sender, EventArgs e)
        {
            if (btnClick == 0)
            {
                btnClick += 1;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[GetPollingStationNumber]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@province", provinceDropdown1.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@city", cityDropdown2.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pollingStationNumberDropdown3.AddItem(dt.Rows[i]["station_Number"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MyConnection.Close();
                }
            }
            else
            {
                pollingStationNumberDropdown3.Clear();
                id = "";
                btnClick += 1;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[GetPollingStationNumber]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@province", provinceDropdown1.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@city", cityDropdown2.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pollingStationNumberDropdown3.AddItem(dt.Rows[i]["station_Number"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MyConnection.Close();
                }
            }
        }

        private void pollingStationNumberDropdown3_onItemSelected(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetPollingStationId]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@pollingStationNumber", pollingStationNumberDropdown3.selectedValue.ToString());
                da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = dt.Rows[i]["station_Id"].ToString();
                    MessageBox.Show(id.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            vote_cast vt = new vote_cast();
            pollingStation = pollingStationNumberDropdown3.selectedValue.ToString();
            //pollingStationNumberDropdown3.selectedValue.ToString()
            this.Hide();
            vt.ShowDialog();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            voting_items ass = new voting_items();
            this.Hide();
            ass.ShowDialog();
        }
    }
}
