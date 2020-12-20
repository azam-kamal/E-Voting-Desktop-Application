using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using libzkfpcsharp;
using Sample;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using EMS;
using System.IO;

namespace EMS
{
    public partial class dashboard : Form
   //Device Init Components
    {

        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool bIdentify = false;
        byte[] FPBuffer;
        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        private int mfpWidth = 0;
        private int mfpHeight = 0;
        private int mfpDpi = 0;
        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public dashboard()
        {    
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 1000;

        }
        //Current print
        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
                Thread.Sleep(200);
            }
        }

        SqlCommand command1, command, command2;

        bool found = false;
        bool big75 = false;
        string marking = "Present";
        SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=ems;Integrated Security=True");
        SqlDataAdapter adap, adap2;
        DataSet ds, dss;
        SqlCommand cmd1;
        SqlCommand cmd2;
        SqlCommand cmd3;
        int id=0;
        int che = 0, sal = 0, over = 0, mark = 1,caal=0,bonus=0;
        string f = "", l = "", cnic = "", mob = "", date = "", starttime = "";

        public void fingermatch()
        {

            ds = new DataSet();
            con.Open();
            adap = new SqlDataAdapter("select *  from fingerprint", con);
            adap.Fill(ds, "fingerprint");
            int count = ds.Tables[0].Rows.Count;
            con.Close();

            for (int i = 0; i < count; i++)
            {
                string blobb = ds.Tables[0].Rows[i]["f_print"].ToString();
                byte[] regTemplate = zkfp.Base64String2Blob(blobb);
                int ma = 0;
                che = zkfp2.DBMatch(mDBHandle, CapTmp, regTemplate);

                if (che >= 75)
                {

                   
                    id = Convert.ToInt32(ds.Tables[0].Rows[i]["emp_id"]);
                    byte[] paramValue1 = new byte[4];
                    zkfp.Int2ByteArray(1, paramValue1);
                    zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                    Thread.Sleep(300);
                    zkfp.Int2ByteArray(0, paramValue1);
                    zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                    byte[] paramValue2 = new byte[4];
                    zkfp.Int2ByteArray(1, paramValue2);
                    zkfp2.SetParameters(mDevHandle, 102, paramValue2, 4);
                    Thread.Sleep(2000);
                    zkfp.Int2ByteArray(0, paramValue2);
                    zkfp2.SetParameters(mDevHandle, 102, paramValue2, 4);
                    break;
                    

                }
            }
                if (che < 74)
                {
                    id = 0;
                    mark = 0;
                    byte[] paramValue1 = new byte[4];
                    zkfp.Int2ByteArray(1, paramValue1);
                    zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                    Thread.Sleep(2000);
                    zkfp.Int2ByteArray(0, paramValue1);
                    zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                    byte[] paramValue2 = new byte[4];
                    zkfp.Int2ByteArray(1, paramValue2);
                    zkfp2.SetParameters(mDevHandle, 103, paramValue2, 4);
                    Thread.Sleep(2000);
                    zkfp.Int2ByteArray(0, paramValue2);
                    zkfp2.SetParameters(mDevHandle, 103, paramValue2, 4);
                    textRes.AppendText("Identification Not Found!" + "\n");
                    textRes.ScrollToCaret();


            }
            
        }
        int hr = 0;
        bool checkin = false;
        public void check_checkin()  // Basically Checkout
        {
           
            string dateee = Convert.ToString(dateTimePicker2.Text);
            adap2 = new SqlDataAdapter("[idandtime]", con);
            adap2.SelectCommand.Parameters.AddWithValue("@date", dateee);
            adap2.SelectCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            dss = new DataSet();
            adap2.Fill(dss);
            con.Close();
            int count2 = dss.Tables[0].Rows.Count;

            if (count2 > 0)
            {

                for (int j = 0; j < count2; j++)
                {
                    int checkid = Convert.ToInt32(dss.Tables[0].Rows[j]["emp_id"]);
                    if (checkid == id)
                    {
                        if (type == "Contract")
                        {

                            
                            starttime = dss.Tables[0].Rows[j]["starttime"].ToString();

                            found = true;
                            DateTime startttime, endtime, final;
                            startttime = Convert.ToDateTime(starttime);
                            
                            endtime = Convert.ToDateTime(label14.Text);

                            TimeSpan timediff;

                            if (startttime > endtime)
                            {
                                timediff = new TimeSpan(startttime.Ticks - endtime.Ticks);

                            }
                            else
                            {
                                timediff = new TimeSpan(endtime.Ticks - startttime.Ticks);

                            }
                            final = Convert.ToDateTime(timediff.ToString());

                            int cal = Convert.ToInt32(final.ToString("HH"));
                            SqlCommand cmd = new SqlCommand("select count(*) from checkout_replica where emp_id=" + id, con);
                            con.Open();
                            
                            int bon = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (bon == 0)
                            {
                                caal = Math.Abs(cal);
                                bonus = 0;
                                
                            }
                            else
                            {
                                SqlCommand cmdd = new SqlCommand("select bonus from employee where emp_id=" + id, con);
                                con.Open();
                                int bonn = Convert.ToInt32(cmdd.ExecuteScalar());
                                con.Close();
                                caal = Math.Abs(cal);
                                bonus = bonn * caal;
                                //SqlCommand cmddd = new SqlCommand("update checkout set endtime=" + endtime + ",rewardedbonus="+bonus,con);
                                con.Open();
                                SqlCommand cmdddd = new SqlCommand("[delete_from_checkout]", con);
                                cmdddd.Parameters.AddWithValue("@id", id);
                                cmdddd.Parameters.AddWithValue("@dat", dateee);
                                cmdddd.CommandType = CommandType.StoredProcedure;
                                cmdddd.ExecuteNonQuery();
                                con.Close();
                                
                            }
                            
                               
                         }
                        else
                        {
                            // textRes.AppendText("ID found in checkin\n");
                            starttime = dss.Tables[0].Rows[j]["starttime"].ToString();

                            found = true;
                            DateTime startttime, endtime, final;
                            startttime = Convert.ToDateTime(starttime);
                            //MessageBox.Show(starttime);
                            endtime = Convert.ToDateTime(label14.Text);

                            TimeSpan timediff;

                            if (startttime > endtime)
                            {
                                timediff = new TimeSpan(startttime.Ticks - endtime.Ticks);

                            }
                            else
                            {
                                timediff = new TimeSpan(endtime.Ticks - startttime.Ticks);

                            }
                            final = Convert.ToDateTime(timediff.ToString());

                            int cal = Convert.ToInt32(final.ToString("HH"));
                            hr= Convert.ToInt32(final.ToString("HH"));
                            SqlCommand cmd = new SqlCommand("select bonus from employee where emp_id=" + id, con);
                            con.Open();
                            int bon = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (cal == 10)
                            {
                                
                                caal = Math.Abs(cal);
                            }
                            else if (cal >= 10)
                            {
                                
                                cal -= 10;
                                caal = Math.Abs(cal);

                                bonus = caal * bon;
                                
                            }
                            else if (cal < 10)
                            {
                                
                                cal -= 10;
                                caal = Math.Abs(cal);
                                
                            }
                        }
                        

                        //filling checkout and replica
                        con.Open();
                        command = new SqlCommand("[attendance_procedure_checkout]", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fname", f);
                        command.Parameters.AddWithValue("@lname", l);
                        command.Parameters.AddWithValue("@cnic", cnic);
                        command.Parameters.AddWithValue("@mobilenumber", mob);
                        command.Parameters.AddWithValue("@sal", sal);
                        command.Parameters.AddWithValue("@ovrtmbonus", bonus);
                        command.Parameters.AddWithValue("@date", dateTimePicker2.Text);
                        command.Parameters.AddWithValue("@startime", starttime);
                        command.Parameters.AddWithValue("@endtime", label14.Text);
                        command.Parameters.AddWithValue("@mark",marking );
                        command.Parameters.AddWithValue("@hour", hr);
                        command.Parameters.AddWithValue("@empid", id);
                        textRes.AppendText(f + " " + l + " has being Checked out!\n");
                        textRes.ScrollToCaret();


                        try
                        {
                            command.ExecuteNonQuery();
                            command.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            checkin = false;
                        }
                        finally
                        {
                            checkin = true;
                            con.Close();
                        }

                        //replica
                        con.Open();
                        command = new SqlCommand("[attendance_procedure_checkout_replica]", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fname", f);
                        command.Parameters.AddWithValue("@lname", l);
                        command.Parameters.AddWithValue("@cnic", cnic);
                        command.Parameters.AddWithValue("@mobilenumber", mob);
                        command.Parameters.AddWithValue("@sal", sal);
                        command.Parameters.AddWithValue("@ovrtmbonus", bonus);
                        command.Parameters.AddWithValue("@date", dateTimePicker2.Text);
                        command.Parameters.AddWithValue("@startime", starttime);
                        command.Parameters.AddWithValue("@endtime", label14.Text);
                        command.Parameters.AddWithValue("@mark", marking);
                        command.Parameters.AddWithValue("@hour", caal);
                        command.Parameters.AddWithValue("@empid", id);
                        

                        try
                        {
                            command.ExecuteNonQuery();
                            command.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            checkin = false;
                        }
                        finally
                        {
                            checkin = true;
                            con.Close();
                        }
                        break;

                    }
                }
            }
            else
            {
                checkin = false;
            }

        }

        public void docheckin()
        {
            //textRes.AppendText(id + " not found in Checkin\n");


            //CheckinTable

            con.Open();
            command = new SqlCommand("[attendance_procedure_checkin]", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fname", f);
            command.Parameters.AddWithValue("@lname", l);
            command.Parameters.AddWithValue("@cnic", cnic);
            command.Parameters.AddWithValue("@mobilenumber", mob);
            command.Parameters.AddWithValue("@sal", sal);
            command.Parameters.AddWithValue("@ovrtmbonus", over);
            command.Parameters.AddWithValue("@date", dateTimePicker2.Text);
            command.Parameters.AddWithValue("@startime", label14.Text);
            command.Parameters.AddWithValue("@markid", marking);
            command.Parameters.AddWithValue("@empid", id);

            try
            {

                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            textRes.AppendText(f + " " + l + " is Present today\n");
            textRes.ScrollToCaret();


            //"Score= " + che.ToString() + "\n"

        }
        string type = "";

        //Yahan matching ka Scene he
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                        MemoryStream ms = new MemoryStream();
                        BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                        Bitmap bmp = new Bitmap(ms);
                        this.picFPImg.Image = bmp;

                    
                        //Match!!
                        if (bIdentify)
                        {
                            int ret = zkfp.ZKFP_ERR_OK;
                            // int fid = 0, score = 0;
                             fingermatch();
                            if (id != 0)
                            {
                               // textRes.AppendText(id + " Recvd\n");
                                command1 = new SqlCommand("[empdet2]", con);
                                command1.CommandType = CommandType.StoredProcedure;
                                command1.Parameters.AddWithValue("@id", id);
                                SqlDataReader rdr;
                                con.Open();
                                rdr = command1.ExecuteReader();
                                //read user data here
                                while (rdr.Read())
                                {
                                    f = rdr.GetString(0);
                                    l = rdr.GetString(1);
                                    cnic = rdr.GetString(2);
                                    mob = rdr.GetString(3);
                                    sal = rdr.GetInt32(4);
                                    over = rdr.GetInt32(5);
                                    type = rdr.GetString(6);


                                  // textRes.AppendText(type);
                                }
                                con.Close();

                                check_checkin();

                                if (checkin == false)
                                {
                                    docheckin();                                    
                                }
                                id = 0;
                            }
                            checkin = false; 
                        }
                    }
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }





        //Conection
       // SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=ems;Integrated Security=True");
      

        private void dashboard_Load(object sender, EventArgs e)
        {
           
            //Front Tabs (Count of Employees)
            con.Open();
            
            cmd1 = new SqlCommand("select count(emp_id) from employee",con);
            label13.Text = cmd1.ExecuteScalar().ToString();
            cmd2 = new SqlCommand("select count(emp_id) from employee where emptype=2", con);
            label12.Text = cmd2.ExecuteScalar().ToString();
            cmd3 = new SqlCommand("select count(emp_id) from employee where emptype=1", con);
            label11.Text = cmd3.ExecuteScalar().ToString();
            con.Close();


            


        }

        public void deviceinit()
        {
            FormHandle = this.Handle;

            cmbIdx.Items.Clear();
            int ret = zkfperrdef.ZKFP_ERR_OK;
            if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
            {
                int nCount = zkfp2.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        cmbIdx.Items.Add(i.ToString());
                    }
                    cmbIdx.SelectedIndex = 0;


                }
                else
                {
                    zkfp2.Terminate();
                    MessageBox.Show("No device connected!");
                }
            }
            else
            {
                MessageBox.Show("Initialize fail, ret=" + ret + " !");
            }
            ret = 0;
            //Device Open Hoga Yahan
            //int ret = zkfp.ZKFP_ERR_OK;
            ret = zkfp.ZKFP_ERR_OK;
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(cmbIdx.SelectedIndex)))
            {
                MessageBox.Show("OpenDevice fail");
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                MessageBox.Show("Init DB fail");
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }



            bnIdentify.Enabled = true;
            
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];

            size = 4;
            zkfp2.GetParameters(mDevHandle, 3, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpDpi);

            // textRes.AppendText("reader parameter, image width:" + mfpWidth + ", height:" + mfpHeight + ", dpi:" + mfpDpi + "\n");

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            textRes.AppendText("Device Ready!\n");
            textRes.ScrollToCaret();

        }



        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            employee ass = new employee();
            //this.Hide();
            ass.ShowDialog();
        }

      
       

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            settings ass = new settings();
            //this.Hide();
            ass.ShowDialog();
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
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
        SqlDataAdapter adap3;
        DataSet dsss;
        int iddd = 0,sall=0;
        string contactt = "";
        string dateee = "";

        private void button3_Click(object sender, EventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();

        }

        private void label16_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            Employee_Data_View pay = new Employee_Data_View();
            //this.Hide();
            pay.ShowDialog();
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            Attendance_View att = new Attendance_View();
            //this.Hide();
            att.ShowDialog();
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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bIsTimeToDie = true;

            //zkfp2.Terminate();

            adap3 = new SqlDataAdapter("[panda]", con);
            adap3.SelectCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            dsss = new DataSet();
            adap3.Fill(dsss);
            con.Close();
            int count3 = dsss.Tables[0].Rows.Count;
            for(int i=0; i<count3; i++)
            {
                string absent = "Absent";
                iddd=Convert.ToInt32(dsss.Tables[0].Rows[i]["emp_id"]);
                f = dsss.Tables[0].Rows[i]["firstname"].ToString();
                l= dsss.Tables[0].Rows[i]["lastname"].ToString();
                cnic= dsss.Tables[0].Rows[i]["nic"].ToString();
                contactt=dsss.Tables[0].Rows[i][4].ToString();
                sall = Convert.ToInt32(dsss.Tables[0].Rows[i]["emp_id"]);
                con.Open();
                command = new SqlCommand("[attendance_procedure_checkout]", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@fname", f);
                command.Parameters.AddWithValue("@lname", l);
                command.Parameters.AddWithValue("@cnic", cnic);
                command.Parameters.AddWithValue("@mobilenumber", contactt);
                command.Parameters.AddWithValue("@sal", 0);
                command.Parameters.AddWithValue("@ovrtmbonus", 0);
                command.Parameters.AddWithValue("@date", dateTimePicker2.Text);
                command.Parameters.AddWithValue("@startime", "-");
                command.Parameters.AddWithValue("@endtime", "-");
                command.Parameters.AddWithValue("@mark",absent);
                command.Parameters.AddWithValue("@hour", 0);
                command.Parameters.AddWithValue("@empid", iddd);
                textRes.AppendText(f + " " + l + " is absent today!\n");
                textRes.ScrollToCaret();


                try
                {
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    checkin = false;
                }
                finally
                {
                    checkin = true;
                    con.Close();
                }


            }
            con.Open();
            SqlCommand cmdd = new SqlCommand("delete from checkout_replica", con);
            try
            {
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            finally
            {
                
                con.Close();
            }


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
            bIsTimeToDie = true;
            zkfp2.Terminate();
            totalemployee s = new totalemployee();
            //this.Hide();
            s.ShowDialog();
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            totalsalemp s = new totalsalemp();
            //this.Hide();
            s.ShowDialog();
        }

        private void bunifuCards2_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            totalconemp s = new totalconemp();
            //this.Hide();
            s.ShowDialog();

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

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            EMS.enroll ff = new EMS.enroll();
            this.Hide();
            ff.ShowDialog();
        }

        private void bnIdentify_Click(object sender, EventArgs e)
        {
            zkfp2.Terminate();
            //Device Initialize Horaa
            deviceinit();

            if (!bIdentify)
            {
                bIdentify = true;
                textRes.AppendText("Attendance Started!\n");
                textRes.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zkfp2.Terminate();
            deviceinit();
            textRes.AppendText("Device Restarted!\n");
            textRes.ScrollToCaret();
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //label14.Text = DateTime.Now.ToLongTimeString();
            label14.Text=DateTime.Now.ToString("HH:mm");
        }
    }
}
