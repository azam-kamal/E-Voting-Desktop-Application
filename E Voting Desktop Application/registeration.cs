﻿using System;
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
    public partial class registeration : Form
    {
        IFirebaseClient client;
        public registeration()
        {
            InitializeComponent();
        }

        IFirebaseConfig ifc = new FirebaseConfig()
        {

            AuthSecret = "cK37opwuVyVL265SeIDQCHm7GVLQsOVZDmvh2U6I",
            BasePath = "https://election-system-database.firebaseio.com/"

        };

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
            regs_items dd = new regs_items();
            this.Hide();
            this.Dispose();
            dd.ShowDialog();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            var voterIdKey = FirebaseKeyGenerator.Next();
            VoterRegs vrs = new VoterRegs()
            {
                VoterExpiryDate = DateTime.Now,
                VoterId = voterIdKey,
                VoterMobileNumber = bunifuMaterialTextbox3.Text,
                VoterNicNumber = bunifuMaterialTextbox2.Text
            };

            VoterInfo vi = new VoterInfo()
            {
                VoterNicNumber = bunifuMaterialTextbox2.Text,
                VoterName = bunifuMaterialTextbox1.Text,
                VoterMobileNumber = bunifuMaterialTextbox3.Text,
                VoterHalkaNumber = bunifuDropdown1.selectedValue,
                VoterAddress = bunifuMaterialTextbox5.Text,
                VoterProvince=bunifuDropdown3.selectedValue,
                VoterCity=bunifuDropdown2.selectedValue,
                ProvincialAssemblyVoterCast = false,
                NationalAssemblyVoterCast = false

            };

            var authKey = FirebaseKeyGenerator.Next();


            var sendToAuth = client.Set(@"/auth/" + authKey, vrs);
            var sendToVoters = client.Set(@"/voters/" + voterIdKey, vi);

            MessageBox.Show("Answer from Auth: " + sendToAuth.ToString() + '\n' + "Answer from Voter: " + sendToVoters.ToString());
        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {

           

            if (bunifuDropdown3.selectedValue =="Sindh")
            {
                bunifuDropdown2.AddItem("Karachi");
                bunifuDropdown2.AddItem("Hyderabad");
                bunifuDropdown2.AddItem("Larkana");
                bunifuDropdown2.AddItem("Sukkur");
            }
            else if(bunifuDropdown3.selectedValue == "Baluchistan")
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
