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
    public partial class voting_items : Form
    {
        public voting_items()
        {
            InitializeComponent();
        }

        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");

       // SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");

        SqlCommand command;
        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            
            groupBox1.Visible = true;
            
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            dashboard ass = new dashboard();
            this.Hide();
            ass.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String getSwicth="";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetElectionDetail]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    getSwicth = dt.Rows[i]["Election_Switch"].ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            if (getSwicth == "1")
            {
                if (maskedTextBox1.Text == "admin")
                {
                    voting_place ass = new voting_place();
                    this.Hide();
                    ass.ShowDialog();
                }
                else
                {
                    MessageBox.Show("password was incorrect");
                }
            }
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {

            command = new SqlCommand("[ElectionSwitchON]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            try

            {
                MyConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                command.Parameters.AddWithValue("@responseMessage", "Sort Of Connection Error");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
            try
            {
                MyConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
            } 
            catch (Exception ex)
            {
                command.Parameters.AddWithValue("@responseMessage", "Sort Of Connection Error");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }


        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            
            command = new SqlCommand("[ElectionSwitchOFF]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                MyConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                command.Parameters.AddWithValue("@responseMessage", "Sort Of Connection Error");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }

        }
    }
}
