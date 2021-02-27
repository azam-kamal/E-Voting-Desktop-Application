using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace E_Voting_Desktop_Application
{
    class connection
    {   
        SqlConnection con = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=E_VOTING_DATABASE;Integrated Security=True");
    }
}
