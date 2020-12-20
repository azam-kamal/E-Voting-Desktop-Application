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

namespace E_Voting_Desktop_Application
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }
//Connection
        SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=ems;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataSet ds;
        bool oldu, newu;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
           // dashboard f2 = new dashboard();
            //f2.ShowDialog();
        }

        private void admin_settings_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            //dashboard f2 = new dashboard();
            //f2.ShowDialog();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            //dashboard f2 = new dashboard();
            //f2.ShowDialog();
        }
//Some Design (GUI)
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            old_textBox.Visible = true;
            new_textBox.Visible = true;
            save_btn_user.Visible = true;
            old_passtextBox2.Visible = false;
            new_pass_textBox.Visible = false;
            save_btn_pass.Visible = false;
            pictureBox6.Image = null;
            pictureBox7.Image = null;
            
        }

   //validation
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(old_passtextBox2.Text))
            {
                pictureBox6.Image = Properties.Resources.Warning;
            }
            else
            {
                pictureBox6.Image = Properties.Resources._checked;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(old_textBox.Text==""&&new_textBox.Text=="")
            {
                pictureBox2.Image = Properties.Resources.Warning;
                pictureBox5.Image = Properties.Resources.Warning;
            }
            else if(old_textBox.Text=="")
            {
                pictureBox2.Image = Properties.Resources.Warning;
            }
            else if(new_textBox.Text=="")
            {
                pictureBox5.Image = Properties.Resources.Warning;
            }
            else if(oldu==true)
            {

            }
            else if(newu==true)
            {

            }
            else{
                try
                {
                    //Data Retrival from database
                    //Username Reset
                    ds = new DataSet();
                    con.Open();
                    adap = new SqlDataAdapter("select *  from login", con);
                    adap.Fill(ds, "login");

                    string username1 = ds.Tables[0].Rows[0]["username"].ToString();
                    string password1 = ds.Tables[0].Rows[0]["password"].ToString();
                    if (old_textBox.Text != username1)
                    {
                        MessageBox.Show("Old Username incorrect");
                    }
                    else
                    {

                        using (cmd = new SqlCommand("[updateusername]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@user", new_textBox.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Username Changed Successfully");

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }//

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            old_passtextBox2.Visible = true;
            new_pass_textBox.Visible = true;
            save_btn_pass.Visible = true;
            old_textBox.Visible = false;
            new_textBox.Visible = false;
            save_btn_user.Visible = false;
            pictureBox2.Image = null;
            pictureBox5.Image = null;
        }
        //validation
        private void button2_Click(object sender, EventArgs e)
        {
            if (old_passtextBox2.Text == "" && new_pass_textBox.Text == "")
            {
                pictureBox6.Image = Properties.Resources.Warning;
                pictureBox7.Image = Properties.Resources.Warning;
            }
            else if (old_passtextBox2.Text == "")
            {
                pictureBox6.Image = Properties.Resources.Warning;
            }
            else if (new_pass_textBox.Text == "")
            {
                pictureBox7.Image = Properties.Resources.Warning;
            }
            else
            {
//Password Reset
                try
                {
                    ds = new DataSet();
                    con.Open();
                    adap = new SqlDataAdapter("select *  from login", con);
                    adap.Fill(ds, "login");
                    string password1 = ds.Tables[0].Rows[0]["password"].ToString();
                    if (old_passtextBox2.Text != password1)
                    {
                        MessageBox.Show("Old password incorrect");
                    }
                    else
                    {
                        using (cmd = new SqlCommand("[updatepassword]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@pass", new_pass_textBox.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Password Changed Successfully");
                            con.Close();
                        }

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Designing
         private void textBox1_Click(object sender, EventArgs e)
        {
            old_textBox.Text = "";
            old_textBox.ForeColor = Color.Black;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            new_textBox.Text = "";
            new_textBox.ForeColor = Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            old_passtextBox2.Text = "";
            old_passtextBox2.ForeColor = Color.Black;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            new_pass_textBox.Text = "";
            new_pass_textBox.ForeColor = Color.Black;
        }

        private void old_textBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(old_textBox.Text, "^[a-zA-Z]+$"))
            {
                pictureBox2.Image = Properties.Resources._checked;
                oldu = false;
            }
            else if (string.IsNullOrEmpty(old_textBox.Text))
            {
                pictureBox2.Image = Properties.Resources.Warning;
                oldu = false;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.cross;
                oldu = true;
            }
        }
        private void new_textBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(new_textBox.Text, "^[a-zA-Z]+$"))
            {
                pictureBox5.Image = Properties.Resources._checked;
                newu = false;
            }
            else if (string.IsNullOrEmpty(new_textBox.Text))
            {
                pictureBox5.Image = Properties.Resources.Warning;
                newu = false;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.cross;
                newu = true;
            }
        }

        private void new_pass_textBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(new_pass_textBox.Text))
            {
                pictureBox7.Image = Properties.Resources.Warning;
            }
            else
            {
                pictureBox7.Image = Properties.Resources._checked;
            }
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.AliceBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.AliceBlue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }
       
    }
}
