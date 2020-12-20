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
   

        private void dashboard_Load(object sender, EventArgs e)
        {
           
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

        private void label4_MouseHover(object sender, EventArgs e)
        {

            pictureBox4.BackColor = Color.AliceBlue;
            
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.AliceBlue;
            
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.AliceBlue;
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.AliceBlue;
        }


        private void label4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
           
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
          
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {

            pictureBox6.BackColor = Color.Transparent;
          
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {

            pictureBox7.BackColor = Color.Transparent;
            
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
      

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

        private void label16_MouseClick(object sender, MouseEventArgs e)
        {
          //  bIsTimeToDie = true;
          //  Employee_Data_View pay = new Employee_Data_View();
            //this.Hide();
          //  pay.ShowDialog();
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.AliceBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void label15_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.AliceBlue;
        }

        private void label15_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Transparent;
        }

        private void label16_MouseHover(object sender, EventArgs e)
        {
            pictureBox11.BackColor = Color.AliceBlue;
        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.BackColor = Color.Transparent;
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
            label14.Text=DateTime.Now.ToString("HH:mm");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            registeration r = new registeration();
            this.Hide();
            r.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
