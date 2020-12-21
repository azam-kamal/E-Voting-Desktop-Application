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
    public partial class candidateRegs : Form
    {
        public candidateRegs()
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

        private void button1_Click(object sender, EventArgs e)
        {
            string repre = "";
            if (checkBox1.Checked == true && checkBox2.Checked==false)
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

            var candidateIdKey = FirebaseKeyGenerator.Next();

            CandidateRegs crs = new CandidateRegs()
            {
                CandidateId = candidateIdKey,
                CandidateName = textBox1.Text,
                CandidateNicNumber = textBox2.Text,
                party = comboBox1.SelectedItem.ToString(),
                representation = repre,
                pollingStation=textBox3.Text
            };


            var key = FirebaseKeyGenerator.Next();


         
            var sendToCandidate = client.Set(@"/candidates/" + key, crs);

            MessageBox.Show("Answer from Auth: " + sendToCandidate.ToString());
        }
    }
}
