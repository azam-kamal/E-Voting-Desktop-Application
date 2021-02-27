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
    public class ConnectionVoter
    {
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;

    public void registerVoter(String voterName,String voterNIC,String voterMobileNumber,String voterProvince,String voterCity,String voterAddress,String voterPollingStationNumber,int voterNationalAssemblyVoteCast,int voterProvincialAssemblyVoteCast)
        {
            command = new SqlCommand("[voters_Stored_Procedure]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@voter_Name", voterName);
            command.Parameters.AddWithValue("@voter_Nic", voterNIC);
            command.Parameters.AddWithValue("@voter_Mobile_Number", voterMobileNumber);
            command.Parameters.AddWithValue("@voter_Province", voterProvince);
            command.Parameters.AddWithValue("@voter_City", voterCity);
            command.Parameters.AddWithValue("@voter_Address", voterAddress);
            command.Parameters.AddWithValue("@voter_Polling_Station_Number", voterPollingStationNumber);
            command.Parameters.AddWithValue("@voter_National_Assembly_Vote_Cast",voterNationalAssemblyVoteCast );
            command.Parameters.AddWithValue("@voter_Provincial_Assembly_Vote_Cast", voterProvincialAssemblyVoteCast);
            command.Parameters.AddWithValue("@responseMessage","Success" );
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
