using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Voting_Desktop_Application
{
    public partial class result_kpk : Form
    {
        public result_kpk()
        {
            InitializeComponent();
        }
        SqlConnection MyConnection = new SqlConnection(@"Data Source=User-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        private void back_btn_Click(object sender, EventArgs e)
        {
            province_result_items ass = new province_result_items();
            this.Hide();
            ass.ShowDialog();
        }

        private void result_kpk_Load(object sender, EventArgs e)
        {
            String voteCount = "", candidateName = "", PartyName = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[GetTopKPK]", MyConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    voteCount = dt.Rows[i]["voteCount"].ToString();
                    candidateName = dt.Rows[i]["candidateName"].ToString();
                    PartyName = dt.Rows[i]["party"].ToString();
                    MessageBox.Show(dt.Rows[i]["voteCount"].ToString());
                }
                label9.Text = PartyName;
                label10.Text = candidateName;
                label11.Text = voteCount;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    }
}
