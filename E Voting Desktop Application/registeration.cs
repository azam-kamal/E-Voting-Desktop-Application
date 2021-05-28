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

        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        String id;
        int btnClick = 0;
        bool fname, ad, nic,mon;
        String provincee = "", cityy = "", partyy = "", pollingg = "";

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
            if (voterNameTextbox1.Text == "" && voterNicTextbox2.Text == "" && addressTextbox3.Text == "" &&voterMobileNumberTextbox4.Text=="")
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
            else if (provincee == "")
            {
                MessageBox.Show("Province is not selected");

            }
            else if (cityy == "")
            {
                MessageBox.Show("City is not selected");

            }

            else if (addressTextbox3.Text == "")
            {
                MessageBox.Show("Address is not selected");
            }
          
            else if (voterMobileNumberTextbox4.Text == "")
            {
                MessageBox.Show("VoterMobileNumber is empty");
            }
            else if (fname == true) { }
            else if (nic == true) { }
            else if (mon == true) { }
            else if (ad == true) { }
            else if (pollingg == "")
            {
                MessageBox.Show("Polling Station is not selected");

            }
            else
            {
                String checkNic = "", checkMob = "";

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[checkVoterCNIC]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@voterNic", voterNicTextbox2.Text);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                      checkNic= dt.Rows[i]["voter_nic"].ToString();
                    }

                }
                catch (Exception ex)
                {
                 MessageBox.Show(ex.ToString());
                }

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("[checkVoterMobileNumber]", MyConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@voterMobileNumber", voterMobileNumberTextbox4.Text);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        checkMob = dt.Rows[i]["voter_mobile_number"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (checkNic == voterNicTextbox2.Text)
                {
                    MessageBox.Show("NIC alredy exists in our database");
                }
                else if (checkMob == voterMobileNumberTextbox4.Text)
                {
                    MessageBox.Show("Mobile Number alredy exists in our database");

                }
                else
                {
                    ConnectionVoter cv = new ConnectionVoter();
                    cv.registerVoter(voterNameTextbox1.Text, voterNicTextbox2.Text, voterMobileNumberTextbox4.Text, provinceDropdown1.selectedValue.ToString(), cityDropdown2.selectedValue.ToString(), addressTextbox3.Text, id, 0, 0);
                    enroll en = new enroll();
                    this.Hide();
                    en.ShowDialog();
                }
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
            provincee = provinceDropdown1.selectedValue.ToString();
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
            pollingg = pollingStationNumberDropdown3.selectedValue.ToString();
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
            cityy = cityDropdown2.selectedValue.ToString();
            }

        private void voterNicTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(voterNicTextbox2.Text, "^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$"))
            {
                cnicPictureBox1.Image = Properties.Resources._checked;
                nic = false;
            }
            else if (string.IsNullOrEmpty(voterNicTextbox2.Text))
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

        private void addressTextbox3_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(addressTextbox3.Text, "^[a-zA-Z]"))
            {
                addresspictureBox1.Image = Properties.Resources._checked;
                ad = false;
            }
            else if (string.IsNullOrEmpty(addressTextbox3.Text))
            {
                addresspictureBox1.Image = Properties.Resources.Warning;
                ad = false;
            }
            else
            {
                addresspictureBox1.Image = Properties.Resources.cross;
                ad = true;
            }
        }

        private void voterMobileNumberTextbox4_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(voterMobileNumberTextbox4.Text, "^[0-9+]{4}-[0-9]{7}$"))
            {
                mobileNumberpictureBox1.Image = Properties.Resources._checked;
                mon = false;
            }
            else if (string.IsNullOrEmpty(voterMobileNumberTextbox4.Text))
            {
                mobileNumberpictureBox1.Image = Properties.Resources.Warning;
                mon = false;
            }
            else
            {
                mobileNumberpictureBox1.Image = Properties.Resources.cross;
                mon = true;
            }
        }

        private void voterNameTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(voterNameTextbox1.Text, "^[a-zA-Z]"))
            {
                namePictureBox3.Image = Properties.Resources._checked;
                fname = false;
            }
            else if (string.IsNullOrEmpty(voterNameTextbox1.Text))
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
    }
        
    
}
