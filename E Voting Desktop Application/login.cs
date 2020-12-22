using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
namespace E_Voting_Desktop_Application
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
          
        }
         bool cross;
        
   //GUI Design
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            username_TxtBox.Text = "";
            username_TxtBox.ForeColor = Color.Black;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            pass_txt_box.Text = "";
            username_TxtBox.ForeColor = Color.Black;
        }

        private void login_Load(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
           
        }
   //validation
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

             if(string.IsNullOrEmpty(username_TxtBox.Text))
            {
                pictureBox2.Image = Properties.Resources.Warning;
                cross = false;
            }
            else
            {
                pictureBox2.Image = Properties.Resources._checked;
                cross = false ;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(pass_txt_box.Text))
            {
                pictureBox3.Image = Properties.Resources.Warning;
            }
            else
            {
                pictureBox3.Image = Properties.Resources._checked;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (username_TxtBox.Text == "" && pass_txt_box.Text == "")
            {
            }
            else if (username_TxtBox.Text == "")
            {
            }
            else if (pass_txt_box.Text == "")
            {
            }
            else if (cross == true)
            {
            }
            else
            {

                string username1 = "admin";
                string password1 = "admin";
                    if ((username_TxtBox.Text == username1 && pass_txt_box.Text == password1))
                    {
                         
                      dashboard f2 = new dashboard();
                    
                    f2.ShowDialog();
        
                    this.Hide();
                        this.Dispose();
                }
                    else
                    {
                        MessageBox.Show("Incorrect username or password");
                    }
                }
            }

        private void exit_btn_MouseHover(object sender, EventArgs e)
        {
            exit_btn.BackColor = Color.LightBlue;
        }
    }
        
    }

