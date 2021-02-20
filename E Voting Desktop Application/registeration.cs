using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
namespace E_Voting_Desktop_Application
{

    public partial class registeration : Form
    {

        SqlConnection MyConnection = new SqlConnection(@"Data Source=USER-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;
        String id;
        int btnClick = 0;
        public registeration()
        {
            InitializeComponent();
        }
        private void registeration_Load(object sender, EventArgs e)
        {
            pollingStationNumberDropdown3.RemoveItem("NA-235");
        }

  

        private void back_btn_Click(object sender, EventArgs e)
        {
            regs_items dd = new regs_items();
            this.Hide();
            this.Dispose();
            dd.ShowDialog();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (voterNameTextbox1.Text == "" && voterNicTextbox2.Text == "" && provinceDropdown1.selectedValue.ToString() == "" && cityDropdown2.selectedValue.ToString() == "" && addressTextbox3.Text == "" && pollingStationNumberDropdown3.selectedValue.ToString() == ""&&voterMobileNumberTextbox4.Text=="")
            {
                MessageBox.Show("All Field Are Empty");
            }
            else if(voterNameTextbox1.Text == "")
            {
                MessageBox.Show("VoterName is empty");
            }
            else if (voterNicTextbox2.Text == "")
            {
                MessageBox.Show("VoterNic is empty");
            }
            else if (provinceDropdown1.selectedValue.ToString() == "")
            {
                MessageBox.Show("Province is not selected");
            }
            else if (cityDropdown2.selectedValue.ToString() == "")
            {
                MessageBox.Show("city is not selected");
            }
            else if (addressTextbox3.Text == "")
            {
                MessageBox.Show("Address is not selected");
            }
            else if (pollingStationNumberDropdown3.selectedValue.ToString()=="")
            {
                MessageBox.Show("PollingStationNumber is not selected");
            }
            else if (voterMobileNumberTextbox4.Text == "")
            {
                MessageBox.Show("VoterMobileNumber is empty");
            }
            else
            {
                ConnectionVoter cv = new ConnectionVoter();
                cv.registerVoter(voterNameTextbox1.Text,voterNicTextbox2.Text,voterMobileNumberTextbox4.Text,provinceDropdown1.selectedValue.ToString(),cityDropdown2.selectedValue.ToString(),addressTextbox3.Text,id,0,0);
            }



        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {
            
            if (provinceDropdown1.selectedValue =="Sindh")
            {
                cityDropdown2.AddItem("Karachi");
                cityDropdown2.AddItem("Hyderabad");
                cityDropdown2.AddItem("Larkana");
                cityDropdown2.AddItem("Sukkur");
            }
            else if(provinceDropdown1.selectedValue == "Baluchistan")
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

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
        }

        private void bunifuDropdown2_onItemSelected(object sender, EventArgs e)

        {
            if (btnClick == 0)
            {
                btnClick+=1;
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
                id ="";
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

        private void voterNameTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
        
    
}
