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
    public partial class pollingStation_regs : Form
    {
       

        private void pollingStation_regs_Load(object sender, EventArgs e)
        {
           /* try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch
            {
                MessageBox.Show("Connection Error");
            }*/
        }

        public pollingStation_regs()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (stationNumberTextBox1.Text == "" && stationNameTextBox2.Text == "" && provinceDropDown1.selectedValue.ToString() == "" && cityDropDown2.selectedValue.ToString() == ""&&addressTextBox3.Text==""&&longitudeTextBox4.Text==""&& latitudeTextBox5.Text=="")
            {
                MessageBox.Show("All Fields Are Left Empty");
            }
            else if(stationNumberTextBox1.Text == "") {
                MessageBox.Show("Station Number is empty");
            }
            else if (stationNameTextBox2.Text == "")
            {
                MessageBox.Show("Station Name is empty");
            }
            else if (provinceDropDown1.selectedValue.ToString() == "")
            {
                MessageBox.Show("province is not selected");
            }
            else if (cityDropDown2.selectedValue.ToString() == "")
            {
                MessageBox.Show("city is not selected");
            }
            else if (addressTextBox3.Text == "")
            {
                MessageBox.Show("address is empty");
            }
            else if (longitudeTextBox4.Text == "")
            {
                MessageBox.Show("longitude is empty");
            }
            else if (latitudeTextBox5.Text == "")
            {
                MessageBox.Show("latitude is empty");
            }
            else
            {
                ConnectionPollingStation cpc = new ConnectionPollingStation();
                cpc.registerPollingStation(stationNumberTextBox1.Text,stationNameTextBox2.Text,provinceDropDown1.selectedValue.ToString(),cityDropDown2.selectedValue.ToString(),addressTextBox3.Text,longitudeTextBox4.Text,latitudeTextBox5.Text);
            }


        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            regs_items ri = new regs_items();
            this.Hide();
            ri.ShowDialog();
        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {
            if (provinceDropDown1.selectedValue == "Sindh")
            {
                cityDropDown2.AddItem("Karachi");
                cityDropDown2.AddItem("Hyderabad");
                cityDropDown2.AddItem("Larkana");
                cityDropDown2.AddItem("Sukkur");
            }
            else if (provinceDropDown1.selectedValue == "Baluchistan")
            {
                cityDropDown2.AddItem("Quetta");
                cityDropDown2.AddItem("Ziarat");
                cityDropDown2.AddItem("Chaman");
                cityDropDown2.AddItem("Sui");
            }
            else if (provinceDropDown1.selectedValue == "Punjab")
            {
                cityDropDown2.AddItem("Lahore");
                cityDropDown2.AddItem("Multan");
                cityDropDown2.AddItem("Faisalabad");
                cityDropDown2.AddItem("Bhawalpur");
            }
            else if (provinceDropDown1.selectedValue == "Khyber Pakhtunkhwa")
            {
                cityDropDown2.AddItem("Peshawar");
                cityDropDown2.AddItem("Mardan");
                cityDropDown2.AddItem("Swat");
                cityDropDown2.AddItem("Abbottabad");
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void stationNumberTextBox1_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
