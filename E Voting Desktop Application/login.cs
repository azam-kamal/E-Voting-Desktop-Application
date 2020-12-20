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
namespace EMS
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
          
        }
//Database Connection
        // SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=ems;Integrated Security=True");
        // SqlDataAdapter adap;
        // DataSet ds;
        // bool cross;
        
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
                //Username and Password Match
                // ds = new DataSet();
                // con.Open();
                // adap = new SqlDataAdapter("select *  from login", con);
                // adap.Fill(ds, "login");

                // if (ds.Tables[0].Rows.Count > 0)
                // {

                //     string username1 = ds.Tables[0].Rows[0]["username"].ToString();
                //     string password1 = ds.Tables[0].Rows[0]["password"].ToString();
                //     con.Close();
                    if ((username_TxtBox.Text == username1 && pass_txt_box.Text == password1))
                    {
                        this.Hide();
                        dashboard f2 = new dashboard();
                        f2.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect username or password");
                    }
                }
            }
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            exit_btn.BackColor = Color.LightBlue;

        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            exit_btn.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.LightGreen;
        }
        
    }
}
