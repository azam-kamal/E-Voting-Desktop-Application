using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Voting_Desktop_Application;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Firebase.Database;
using Firebase.Database.Http;

namespace E_Voting_Desktop_Application
{
    public partial class CandidateRegs : Form
    {
        public CandidateRegs()
        {
            InitializeComponent();
        }

        IFirebaseConfig ifc = new FirebaseConfig()
        {

            AuthSecret = "cK37opwuVyVL265SeIDQCHm7GVLQsOVZDmvh2U6I",
            BasePath = "https://election-system-database.firebaseio.com/"

        };

        IFirebaseClient client;

        private void registeration_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch
            {
                MessageBox.Show("Connection Error");
            }
        }

 

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard dd = new dashboard();
            this.Hide();
            dd.ShowDialog();
        }

    

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            string repre = "";
            if (checkBox1.Checked == true && checkBox2.Checked == false)
            {
                repre = "National";
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == true)
            {
                repre = "Provincial";
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Please Select atleast One Representation");
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                repre = "Both";
            }

            var candidateIdKey = FirebaseKeyGenerator.Next();

            candidateRegs crs = new candidateRegs()
            {
                candidateId = candidateIdKey,
                candidateName = bunifuMaterialTextbox1.Text,
                candidateNicNumber = bunifuMaterialTextbox2.Text,
                age=numericUpDown1.Value.ToString(),
                province=bunifuDropdown2.selectedValue,
                city=bunifuDropdown4.selectedValue,
                party = bunifuDropdown3.selectedValue,
                representation = repre,
                pollingStation = bunifuDropdown1.selectedValue
            };


            var key = FirebaseKeyGenerator.Next();



            var sendToCandidate = client.Set(@"/candidates/" + key, crs);

            MessageBox.Show("Answer from Auth: " + sendToCandidate.ToString());
        }

        private void bunifuDropdown2_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown2.selectedValue == "Sindh")
            {
                bunifuDropdown4.AddItem("Karachi");
                bunifuDropdown4.AddItem("Hyderabad");
                bunifuDropdown4.AddItem("Larkana");
                bunifuDropdown4.AddItem("Sukkur");
            }
            else if (bunifuDropdown2.selectedValue == "Baluchistan")
            {
                bunifuDropdown4.AddItem("Quetta");
                bunifuDropdown4.AddItem("Ziarat");
                bunifuDropdown4.AddItem("Chaman");
                bunifuDropdown4.AddItem("Sui");
            }
            else if (bunifuDropdown2.selectedValue == "Punjab")
            {
                bunifuDropdown4.AddItem("Lahore");
                bunifuDropdown4.AddItem("Multan");
                bunifuDropdown4.AddItem("Faisalabad");
                bunifuDropdown4.AddItem("Bhawalpur");
            }
            else if (bunifuDropdown2.selectedValue == "Khyber Pakhtunkhwa")
            {
                bunifuDropdown4.AddItem("Peshawar");
                bunifuDropdown4.AddItem("Mardan");
                bunifuDropdown4.AddItem("Swat");
                bunifuDropdown4.AddItem("Abbottabad");
            }
        }
    }
}
