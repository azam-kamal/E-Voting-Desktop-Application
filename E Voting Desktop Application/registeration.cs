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
    public partial class registeration : Form
    {
        public registeration()
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

        private void button1_Click(object sender, EventArgs e)
        {
            var voterIdKey = FirebaseKeyGenerator.Next();
            VoterRegs vrs = new VoterRegs()
            {
                VoterExpiryDate = DateTime.Now,
                VoterId = voterIdKey,
                VoterMobileNumber = textBox3.Text,
                VoterNicNumber = textBox2.Text
            };

            VoterInfo vi = new VoterInfo()
            {
                VoterNicNumber = textBox2.Text,
                VoterName = textBox1.Text,
                VoterMobileNumber = textBox3.Text,
                VoterHalkaNumber = textBox4.Text,
                VoterAddress = textBox5.Text,
                ProvincialAssemblyVoterCast = false,
                NationalAssemblyVoterCast = false

            };

            var authKey = FirebaseKeyGenerator.Next();


            var sendToAuth = client.Set(@"/auth/" + authKey, vrs);
            var sendToVoters = client.Set(@"/voters/" + voterIdKey, vi);

            MessageBox.Show("Answer from Auth: " + sendToAuth.ToString() + '\n' + "Answer from Voter: " + sendToVoters.ToString());

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard dd = new dashboard();
            this.Hide();
            dd.ShowDialog();
        }
    }
}
