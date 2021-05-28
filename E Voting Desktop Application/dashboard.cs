using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//using libzkfpcsharp;
//using Sample;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Threading;
using E_Voting_Desktop_Application;
using System.IO;

namespace E_Voting_Desktop_Application
{
  
    public partial class dashboard : Form {

        //Device Init Components
        public dashboard()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 1000;

        }
        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        private void dashboard_Load(object sender, EventArgs e)
        {
            getSindhTotalVotes();
            getPunjabTotalVotes();
            getBaluchistanTotalVotes();
            getKpkTotalVotes();
            //Front Tabs (Count of Employees)
      

        }

       
        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
           
            //this.Hide();
            //ass.ShowDialog();
        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
           
            settings ass = new settings();
            //this.Hide();
            ass.ShowDialog();
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
           
            login f1 = new login();
            this.Hide();
            f1.ShowDialog();
            
        }

       

        private void bunifuCards1_MouseHover(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.AliceBlue;
            
        }

        private void bunifuCards1_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.FromArgb(192, 192, 255);
          
        }

        private void bunifuCards2_MouseHover(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.AliceBlue;
        }

        private void bunifuCards2_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void bunifuCards3_MouseHover(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.AliceBlue;
        }

        private void bunifuCards3_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.FromArgb(192, 192, 255);
        }

        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
      

        private void label16_MouseClick(object sender, MouseEventArgs e)
        {
          //  bIsTimeToDie = true;
          //  Employee_Data_View pay = new Employee_Data_View();
            //this.Hide();
          //  pay.ShowDialog();
        }

        
        private void bunifuCards1_MouseHover_1(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.AliceBlue;
        }

        private void bunifuCards1_MouseLeave_1(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void bunifuCards2_MouseHover_1(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.AliceBlue;
        }

        private void bunifuCards2_MouseLeave_1(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void bunifuCards3_MouseHover_1(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.AliceBlue;
        }

        private void bunifuCards3_MouseLeave_1(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void bunifuCards3_MouseClick(object sender, MouseEventArgs e)
        {
        //   bIsTimeToDie = true;
        //    zkfp2.Terminate();
        //    totalemployee s = new totalemployee();
            //this.Hide();
         //   s.ShowDialog();
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
         //   bIsTimeToDie = true;
         //   zkfp2.Terminate();
         //   totalsalemp s = new totalsalemp();
            //this.Hide();
         //   s.ShowDialog();
        }

        private void bunifuCards2_MouseClick(object sender, MouseEventArgs e)
        {
          //  bIsTimeToDie = true;
          //  zkfp2.Terminate();
          //  totalconemp s = new totalconemp();
            //this.Hide();
         //   s.ShowDialog();

        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
           /* bIsTimeToDie = true;
            zkfp2.Terminate();
            main_attendance m = new main_attendance();
            //this.Hide();
            m.ShowDialog();
            */
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            //label14.Text = DateTime.Now.ToLongTimeString();
            //label14.Text=DateTime.Now.ToString("HH:mm:ss");
            label14.Text = DateTime.Now.ToLongDateString()+"\n"+DateTime.Now.ToString("HH:mm:ss");
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            regs_items ri = new regs_items();
            this.Hide();
            ri.ShowDialog();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            settings ass = new settings();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTileButton5_Click(object sender, EventArgs e)
        {
            voting_items ass = new voting_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            result_items ass = new result_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void getSindhTotalVotes()
        {
            String getSindhVotes = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[CountSindhVotes]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    getSindhVotes = dt.Rows[i]["sindhVotes"].ToString();
                }
                sindhVotes.Text = getSindhVotes.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void getPunjabTotalVotes()
        {
            String getPunjabVotes = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[CountPunjabVotes]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    getPunjabVotes = dt.Rows[i]["punjabVotes"].ToString();
                }
               punjabVotes.Text = getPunjabVotes.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void getBaluchistanTotalVotes()
        {
            String getBaluchistanVotes = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[CountBaluchistanVotes]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    getBaluchistanVotes = dt.Rows[i]["baluchistanVotes"].ToString();
                }
                baluchistanVotes.Text = getBaluchistanVotes.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void getKpkTotalVotes()
        {
            String getkpkVotes = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[CountkpkVotes]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    getkpkVotes = dt.Rows[i]["kpkVotes"].ToString();
                }
                kpkVotes.Text = getkpkVotes.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
