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
    public partial class voter_cast3 : Form
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

        public voter_cast3()
        {
            InitializeComponent();
        }

        public voter_cast3(string v1, string v2, string text1, string v, string text2)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.text1 = text1;
            this.v3 = v3;
            this.text2 = text2;
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
        private string v1;
        private string v2;
        private string text1;
        private string v3;
        private string text2;

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

                                        MessageBox.Show("YOUR VOTE HAS BEING CASTED!\n");

                                    //DATABSE ME ADD KARNA HE IDHAR SE
                                    bIsTimeToDie = true;
                                    zkfp2.Terminate();
                                    vote_cast d = new vote_cast();
                                    this.Hide();
                                    d.ShowDialog();
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
                                    MessageBox.Show("Identification Unsuccessfull! \nScore= " + che.ToString() + "\n"+"Please try Again"+"\n"+"If Problem persisted, Contact nearby agents");
                                bIsTimeToDie = true;
                                zkfp2.Terminate();
                                vote_cast d = new vote_cast();
                                this.Hide();
                                d.ShowDialog();
                            }

                                con.Close();

                            }
                        
                    }
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

       

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            if (!bIdentify)
            {
                bIdentify = true;
                textRes.AppendText("Please press your finger!\n");
            }
        }

        private void voter_cast3_Load(object sender, EventArgs e)
        {
            //Values load up

            label7.Text = v2;
            label12.Text = v3;
            label8.Text = text1;
            label11.Text = text2;
            //////////////////////////////////////////

            SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
            SqlCommand cmd;
            cmd = new SqlCommand("[lastvoter]", MyConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@cnic", cnic);

            try
            {
                MyConnection.Open();
                var voterID = cmd.ExecuteScalar().ToString();
            //    label4.Text = voterID;
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
            ret = 0;
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
            textRes.AppendText("PLEASE FOLLOW THE INSTRUCTIONS\n1. Click on Cast Vote\n2. Apply your registered fingerprint on the device\n");

        }
    }
    
    }

