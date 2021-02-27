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
    public class ConnectionPollingStation
    {
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

        public void registerPollingStation(String stationNumber,String name,String province,String city,String address,String longitude,String latitude)
        {
            command = new SqlCommand("[PollingStation_Registration-Stored_Procedure]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@station_Number",stationNumber );
            command.Parameters.AddWithValue("@station_Name", name);
            command.Parameters.AddWithValue("@province", province);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@longitude", longitude);
            command.Parameters.AddWithValue("@latitude", latitude);
            command.Parameters.AddWithValue("@responseMessage", "Success");

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
