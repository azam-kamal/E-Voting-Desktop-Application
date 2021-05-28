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
    public partial class CandidateRegs : Form
    {
        public CandidateRegs()
        {
            InitializeComponent();
        }

        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;
        String representation;
        String id;
        int btnClick = 0;
        bool fname;
        bool nic;
        String provincee= "", cityy= "", partyy="", pollingg ="";
        private void registeration_Load(object sender, EventArgs e)
        {
            pollingStationDropDown3.RemoveItem("NA-245");
            pollingStationDropDown3.RemoveItem("NA-255");


        }



        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard dd = new dashboard();
            this.Hide();
            dd.ShowDialog();
        }



        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            
            if (fullNameTextBox1.Text == "" &&  pollingg==""&&provincee==""&&cityy==""&&partyy==""&& CNICTextBox2.Text == "" && AgeNumericUpDown1.Value.ToString() == ""  && nationalAssemblyRadioButton1.Checked == false &&provincialAssemblyRadioButton1.Checked== false )
            {
                MessageBox.Show("All Field Are Empty");

            }
            else if (fullNameTextBox1.Text == "")
            {
                MessageBox.Show("Candidate Name is empty");

            }
            
            else if (CNICTextBox2.Text == "")
            {
                MessageBox.Show("Candidate NIC is empty");

            }
            else if (AgeNumericUpDown1.Value.ToString() == "")
            {
                MessageBox.Show("Candidate Age is not selected");

            }
            else if (provincee == "")
            {
                MessageBox.Show("Province is not selected");

            }
            else if (cityy == "")
            {
                MessageBox.Show("City is not selected");

            }
         
            else if (nationalAssemblyRadioButton1.Checked == false && provincialAssemblyRadioButton1.Checked == false)
            {
                MessageBox.Show("Representation checkbox not checked");

            }
          else if (fname == true) { }
          else if (nic == true) { }
        else if (pollingg == "")
            {
                MessageBox.Show("Polling is not selected");

            }
            else
            {
                String checkNic = "";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[checkCandidateCNIC]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@candidateNic", CNICTextBox2.Text);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        checkNic = dt.Rows[i]["candidate_cnic"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (checkNic == CNICTextBox2.Text)
                {
                    MessageBox.Show("CNIC already exists in the database");
                }
                else
                {
                    if (nationalAssemblyRadioButton1.Checked == true)
                    {
                        representation = "National Assembly";



                        ConnectionCandidates cc = new ConnectionCandidates();
                        cc.registerCandidate(fullNameTextBox1.Text, CNICTextBox2.Text, AgeNumericUpDown1.Value.ToString(), provinceDropdown1.selectedValue.ToString(), cityDropDown2.selectedValue.ToString(), id, partyDropDown3.selectedValue.ToString(), representation);
                    }
                    else
                    {
                        representation = "Provincial Assembly";
                        ConnectionCandidates cc = new ConnectionCandidates();
                        cc.registerCandidate(fullNameTextBox1.Text, CNICTextBox2.Text, AgeNumericUpDown1.Value.ToString(), provinceDropdown1.selectedValue.ToString(), cityDropDown2.selectedValue.ToString(), id, partyDropDown3.selectedValue.ToString(), representation);

                    }
                }

            }


        }

        private void bunifuDropdown2_onItemSelected(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }






        private void provinceDropdown1_onItemSelected(object sender, EventArgs e)
        {
            
        }

        private void cityDropdown2_onItemSelected_1(object sender, EventArgs e)
        {
            String province = provinceDropdown1.selectedValue.ToString();
            String city = cityDropDown2.selectedValue.ToString();
            if (btnClick == 0)
            {
                btnClick += 1;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[GetPollingStationNumber]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@province", province);
                    da.SelectCommand.Parameters.AddWithValue("@city", city);
                    da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pollingStationDropDown3.AddItem(dt.Rows[i]["station_Number"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show(provinceDropdown1.selectedValue.ToString());

                    MessageBox.Show(cityDropDown2.selectedValue.ToString());

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MyConnection.Close();
                }
            }
            else
            {
                pollingStationDropDown3.Clear();
                id = "";
                btnClick += 1;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[GetPollingStationNumber]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@province", provinceDropdown1.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@city", cityDropDown2.selectedValue.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pollingStationDropDown3.AddItem(dt.Rows[i]["station_Number"].ToString());
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
            cityy = cityDropDown2.selectedValue.ToString();
        }
        private void fullNameTextBox1_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(fullNameTextBox1.Text, "^[a-zA-Z]"))
            {
                namePictureBox3.Image = Properties.Resources._checked;
                fname = false;
            }
            else if (string.IsNullOrEmpty(fullNameTextBox1.Text))
            {
                namePictureBox3.Image = Properties.Resources.Warning;
                fname = false;
            }
            else
            {
                namePictureBox3.Image = Properties.Resources.cross;
                fname = true;
            }
        }

        private void partyDropDown3_onItemSelected(object sender, EventArgs e)
        {
            partyy = partyDropDown3.selectedValue.ToString();
        }

        private void CNICTextBox2_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(CNICTextBox2.Text, "^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$"))
            {
                cnicPictureBox1.Image = Properties.Resources._checked;
                nic = false;
            }
            else if (string.IsNullOrEmpty(CNICTextBox2.Text))
            {
                cnicPictureBox1.Image = Properties.Resources.Warning;
                nic = false;
            }
            else
            {
                cnicPictureBox1.Image = Properties.Resources.cross;
                nic = true;
            }
        }
        private void pollingStationDropDown3_onItemSelected(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetPollingStationId]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@pollingStationNumber", pollingStationDropDown3.selectedValue.ToString());
                da.SelectCommand.Parameters.AddWithValue("@responseMessage", "Success");
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = dt.Rows[i]["station_Id"].ToString();
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
            pollingg = pollingStationDropDown3.selectedValue.ToString();
        }

        private void provinceDropdown1_onItemSelected_1(object sender, EventArgs e)
        {
            if (provinceDropdown1.selectedValue == "Sindh")
            {

                cityDropDown2.AddItem("Karachi");
                cityDropDown2.AddItem("Hyderabad");
                cityDropDown2.AddItem("Larkana");
                cityDropDown2.AddItem("Sukkur");
            }
            else if (provinceDropdown1.selectedValue == "Baluchistan")
            {
                cityDropDown2.AddItem("Quetta");
                cityDropDown2.AddItem("Ziarat");
                cityDropDown2.AddItem("Chaman");
                cityDropDown2.AddItem("Sui");
            }
            else if (provinceDropdown1.selectedValue == "Punjab")
            {
                cityDropDown2.AddItem("Lahore");
                cityDropDown2.AddItem("Multan");
                cityDropDown2.AddItem("Faisalabad");
                cityDropDown2.AddItem("Bhawalpur");
            }
            else if (provinceDropdown1.selectedValue == "Khyber Pakhtunkhwa")
            {
                cityDropDown2.AddItem("Peshawar");
                cityDropDown2.AddItem("Mardan");
                cityDropDown2.AddItem("Swat");
                cityDropDown2.AddItem("Abbottabad");
            }
            provincee = provinceDropdown1.selectedValue.ToString();
        }
    }
}
