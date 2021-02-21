﻿using System;
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
   public class ConnectionCandidates
    {
        SqlConnection MyConnection = new SqlConnection(@"Data Source=USER-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
        SqlCommand command;
        public void registerCandidate(String name,String nic,String age,String province,String city,String pollingStationNumber,String party,String representation)
        {
            command = new SqlCommand("[Candidates_Registration_Stored_Procedures]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@nic", nic);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@province", province);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@party", party);
            command.Parameters.AddWithValue("@representation", representation);
            command.Parameters.AddWithValue("@pollingStationNumber", pollingStationNumber);
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