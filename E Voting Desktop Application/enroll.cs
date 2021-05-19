using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using libzkfpcsharp;
using Sample;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data;

namespace E_Voting_Desktop_Application
{
    public partial class enroll : Form
    {
       
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        bool bIdentify = false;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];

        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;

        private int mfpWidth = 0;
        private int mfpHeight = 0;
        private int mfpDpi = 0;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public enroll()
        {
            InitializeComponent();
        }


        //Iss Func se Ap Current FingerPrint Uthaa te hen jo recently Press hua ho
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

        string template;
        //Yahan se Database me Add or match ka scene he
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

                        //Enrollment!!!
                        if (IsRegister)
                        {
                            int ret = zkfp.ZKFP_ERR_OK;
                            int fid = 0, score = 0;
                            ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                        
                            if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)
                            {
                                textRes.AppendText("Please press the same finger for 3 times\n");
                                return;
                            }
                           
                            Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);
                            String strBase64 = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
                            byte[] blob = zkfp2.Base64ToBlob(strBase64);
                            RegisterCount++;
                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {
                                RegisterCount = 0;
                                if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp)) &&
                                       zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBAdd(mDBHandle, iFid, RegTmp)))
                                {

                                    template = zkfp2.BlobToBase64(RegTmp, cbRegTmp);
                                    //textRes.AppendText("Please Enter Employee ID and Click Confirm to Register Biometric.\n");
                                    
                                    label4.Enabled = true;

                                    if (label4.Text != "")
                                    {
                                        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
                                        SqlCommand command;

                                        int empid = Convert.ToInt32(label4.Text);
                                        command = new SqlCommand("[addfinger]", MyConnection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@voter_id", label4.Text);
                                        command.Parameters.AddWithValue("f_print", template);

                                        try
                                        {
                                            MyConnection.Open();
                                            command.ExecuteNonQuery();
                                            command.Dispose();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.ToString());
                                        }
                                        finally
                                        {
                                            MyConnection.Close();
                                        }
                                        iFid++;
                                        textRes.AppendText("Biometric Enrolled Successfully\n");
                                        IsRegister = false;
                                        label4.Enabled = true;
                                        MessageBox.Show("Voter Registered Successfully");
                                        bIsTimeToDie = true;
                                        zkfp2.Terminate();
                                        regs_items d = new regs_items();
                                        this.Hide();
                                        d.ShowDialog();

                                       
                                    }
                                    else
                                    {
                                        textRes.AppendText("Incorrect or No ID Found Please Try Again\n");
                                    }



                                    ////////
                                }
                                else
                                {
                                    textRes.AppendText("Enrollment Failed, error code=" + ret + "\n");
                                }
                                
                                return;
                            }
                            else
                            {
                                byte[] paramValue1 = new byte[4];
                                zkfp.Int2ByteArray(1, paramValue1);
                                zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                                Thread.Sleep(100);
                                zkfp.Int2ByteArray(0, paramValue1);
                                zkfp2.SetParameters(mDevHandle, 104, paramValue1, 4);
                                byte[] paramValue2 = new byte[4];
                                zkfp.Int2ByteArray(1, paramValue2);
                                zkfp2.SetParameters(mDevHandle, 102, paramValue2, 4);
                                Thread.Sleep(1000);
                                zkfp.Int2ByteArray(0, paramValue2);
                                zkfp2.SetParameters(mDevHandle, 102, paramValue2, 4);
                                textRes.AppendText("You need to press the " + (REGISTER_FINGER_COUNT - RegisterCount) + " times fingerprint\n");
                            }
                        }
                        
                        else
                        {

                            //Match!!
                            if (bIdentify)
                            {
                                int ret = zkfp.ZKFP_ERR_OK;
                               // int fid = 0, score = 0;

                                SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
                                SqlDataAdapter adap;
                                DataSet ds;
                                ds = new DataSet();
                                con.Open();
                                adap = new SqlDataAdapter("select *  from fingerprint", con);
                                adap.Fill(ds, "fingerprint");
                                int count = ds.Tables[0].Rows.Count;
                                int che = 0;
                                for (int i = 0; i < count; i++)
                                {
                                    string blobb = ds.Tables[0].Rows[i]["f_print"].ToString();
                                    byte[] regTemplate = zkfp.Base64String2Blob(blobb);

                                    che = zkfp2.DBMatch(mDBHandle, CapTmp, regTemplate);
                                    if (che >= 75)
                                    {
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

                                        textRes.AppendText("Identification Successfull! \nScore= " + che.ToString() + "\n");
                                        break;
                                    }

                                }
                                if (che < 74)
                                {
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
                                    textRes.AppendText("Identification Unsuccessfull! \nScore= " + che.ToString() + "\n");
                                }

                                con.Close();

                            }
                        }
                }
                break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
            SqlCommand cmd;
            cmd = new SqlCommand("[lastvoter]", MyConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@cnic", cnic);
            
            try
            {
                MyConnection.Open();
                var voterID=cmd.ExecuteScalar().ToString();
                label4.Text = voterID;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
        
        //Device Initialize Horaa


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
                    bIsTimeToDie = true;
                    zkfp2.Terminate();
                    regs_items d = new regs_items();
                    this.Hide();
                    d.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Initialize fail, ret=" + ret + " !");
                bIsTimeToDie = true;
                zkfp2.Terminate();
                regs_items d = new regs_items();
                this.Hide();
                d.ShowDialog();
            }
            ret=0;
            //Device Open Hoga Yahan
            //int ret = zkfp.ZKFP_ERR_OK;
            ret = zkfp.ZKFP_ERR_OK;
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(cmbIdx.SelectedIndex)))
            {
                MessageBox.Show("OpenDevice fail");
                bIsTimeToDie = true;
                zkfp2.Terminate();
                regs_items d = new regs_items();
                this.Hide();
                d.ShowDialog();
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                MessageBox.Show("Init DB fail");
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                bIsTimeToDie = true;
                zkfp2.Terminate();
                regs_items d = new regs_items();
                this.Hide();
                d.ShowDialog();
            }

            bunifuThinButton22.Enabled = true;

            bunifuThinButton21.Enabled = true;  
            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
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
            textRes.AppendText("Device Ready!\nClick Enroll Biometric and take fingerprints\n");

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.Terminate();
            regs_items d = new regs_items();
            this.Hide();
            d.ShowDialog();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (label4.Text == null)
            {
                textRes.AppendText("NO ID FOUND!\n");
            }
            else
            {

                if (!IsRegister)
                {
                    IsRegister = true;
                    RegisterCount = 0;
                    cbRegTmp = 0;
                    textRes.AppendText("Please press your finger 3 times!\n");
                }
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (!bIdentify)
            {
                bIdentify = true;
                textRes.AppendText("Please press your finger!\n");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
