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

        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;
        String representation;
        String id;
        int btnClick = 0;

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
           
            if (fullNameTextBox1.Text == "" && CNICTextBox2.Text == "" && AgeNumericUpDown1.Value.ToString() == "" && provinceDropdown1.selectedValue.ToString() == "" && cityDropDown2.selectedValue.ToString() == "" && partyDropDown3.selectedValue.ToString() == "" && nationalAssemblyCheckBox1.Checked == false &&provincialAssemblyCheckBox2.Checked==false&&pollingStationDropDown3.selectedValue.ToString()=="") {
                MessageBox.Show("All Field Are Empty");

            }
            else if(fullNameTextBox1.Text == "")
            {
                MessageBox.Show("Candidate Name is empty");

            }
            else if(CNICTextBox2.Text == "")
            {
                MessageBox.Show("Candidate NIC is empty");

            }
            else if (AgeNumericUpDown1.Value.ToString() == "")
            {
                MessageBox.Show("Candidate Age is not selected");

            }
            else if(provinceDropdown1.selectedValue.ToString() == "")
            {
                MessageBox.Show("province is not selected");

            }
            else if(cityDropDown2.selectedValue.ToString() == "")
            {
                MessageBox.Show("city is not selected");

            }
            else if(partyDropDown3.selectedValue.ToString() == "")
            {
                MessageBox.Show("party is not selected");

            }
            else if(nationalAssemblyCheckBox1.Checked == false&& provincialAssemblyCheckBox2.Checked == false)
            {
                MessageBox.Show("Representation checkbox not checked");

            }
            else if(pollingStationDropDown3.selectedValue.ToString() == "")
            {
                MessageBox.Show("pollibgstationnumber is not selected");

            }

            else
            {
                if (nationalAssemblyCheckBox1.Checked == true&&provincialAssemblyCheckBox2.Checked==false)
                {
                    representation = "National Assembly";
                    MessageBox.Show(fullNameTextBox1.Text);
                    MessageBox.Show(CNICTextBox2.Text);
                    MessageBox.Show(AgeNumericUpDown1.Value.ToString());
                    MessageBox.Show(provinceDropdown1.selectedValue.ToString());
                    MessageBox.Show(cityDropDown2.selectedValue.ToString());
                    MessageBox.Show(id);
                    MessageBox.Show(representation);


                    MessageBox.Show(partyDropDown3.selectedValue.ToString());





                    ConnectionCandidates cc = new ConnectionCandidates();
                    cc.registerCandidate(fullNameTextBox1.Text, CNICTextBox2.Text, AgeNumericUpDown1.Value.ToString(), provinceDropdown1.selectedValue.ToString(), cityDropDown2.selectedValue.ToString(), id, partyDropDown3.selectedValue.ToString(), representation);
                }
                else if (nationalAssemblyCheckBox1.Checked==false&&provincialAssemblyCheckBox2.Checked == true)
                {
                    representation = "Provincial Assembly";
                    ConnectionCandidates cc = new ConnectionCandidates();
                    cc.registerCandidate(fullNameTextBox1.Text, CNICTextBox2.Text, AgeNumericUpDown1.Value.ToString(), provinceDropdown1.selectedValue.ToString(), cityDropDown2.selectedValue.ToString(), id, partyDropDown3.selectedValue.ToString(), representation);

                }
                else if (nationalAssemblyCheckBox1.Checked == true && provincialAssemblyCheckBox2.Checked == true)
                {
                    representation = "both";
                    ConnectionCandidates cc = new ConnectionCandidates();
                    cc.registerCandidate(fullNameTextBox1.Text, CNICTextBox2.Text, AgeNumericUpDown1.Value.ToString(), provinceDropdown1.selectedValue.ToString(), cityDropDown2.selectedValue.ToString(), id, partyDropDown3.selectedValue.ToString(), representation);
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
    }
}
