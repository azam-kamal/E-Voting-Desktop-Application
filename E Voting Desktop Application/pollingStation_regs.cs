using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Firebase.Database;
using Firebase.Database.Http;

namespace E_Voting_Desktop_Application
{
    public partial class pollingStation_regs : Form
    {
        IFirebaseClient client;
        public pollingStation_regs()
        {
            InitializeComponent();
        }

        IFirebaseConfig ifc = new FirebaseConfig()
        {

            AuthSecret = "cK37opwuVyVL265SeIDQCHm7GVLQsOVZDmvh2U6I",
            BasePath = "https://election-system-database.firebaseio.com/"

        };

        private void pollingStation_regs_Load(object sender, EventArgs e)
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
        

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            var PollIdKey = FirebaseKeyGenerator.Next();
            PollRegs vrs = new PollRegs()
            {
                StationNo = bunifuMaterialTextbox2.Text,
                PollSatationName = bunifuMaterialTextbox2.Text,
                province=bunifuDropdown3.selectedValue,
                city=bunifuDropdown2.selectedValue,
                PollSatationAddress = bunifuMaterialTextbox3.Text,
                longitutde = bunifuMaterialTextbox4.Text,
                latitude= bunifuMaterialTextbox5.Text
            };

           

            var sendToPoll = client.Set(@"/PollingStation/" + PollIdKey, vrs);
            

            MessageBox.Show("Answer from Auth: " + sendToPoll.ToString());
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            regs_items ri = new regs_items();
            this.Hide();
            ri.ShowDialog();
        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown3.selectedValue == "Sindh")
            {
                bunifuDropdown2.AddItem("Karachi");
                bunifuDropdown2.AddItem("Hyderabad");
                bunifuDropdown2.AddItem("Larkana");
                bunifuDropdown2.AddItem("Sukkur");
            }
            else if (bunifuDropdown3.selectedValue == "Baluchistan")
            {
                bunifuDropdown2.AddItem("Quetta");
                bunifuDropdown2.AddItem("Ziarat");
                bunifuDropdown2.AddItem("Chaman");
                bunifuDropdown2.AddItem("Sui");
            }
            else if (bunifuDropdown3.selectedValue == "Punjab")
            {
                bunifuDropdown2.AddItem("Lahore");
                bunifuDropdown2.AddItem("Multan");
                bunifuDropdown2.AddItem("Faisalabad");
                bunifuDropdown2.AddItem("Bhawalpur");
            }
            else if (bunifuDropdown3.selectedValue == "Khyber Pakhtunkhwa")
            {
                bunifuDropdown2.AddItem("Peshawar");
                bunifuDropdown2.AddItem("Mardan");
                bunifuDropdown2.AddItem("Swat");
                bunifuDropdown2.AddItem("Abbottabad");
            }
        }
    }
}
