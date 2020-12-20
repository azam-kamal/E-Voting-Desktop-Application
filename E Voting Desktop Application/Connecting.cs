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
    public class Connecting
    {
        SqlConnection MyConnection = new SqlConnection(@"Data Source=AZAM-PC;Initial Catalog=ems;Integrated Security=True");
        SqlCommand command;
        public void hireemployee(string cnic, string fn,string ln,int gender,int emptype,int sal, string mobilenum
                                 , string address, string joinyear,int bonus,int adv)
        {
            command = new SqlCommand("[hireemployee]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cnic", cnic);
            command.Parameters.AddWithValue("@finame", fn);
            command.Parameters.AddWithValue("@laname", ln);
            command.Parameters.AddWithValue("@gen", gender);
            command.Parameters.AddWithValue("@emptype",emptype);
            command.Parameters.AddWithValue("@salary", sal);
            command.Parameters.AddWithValue("@mobilenum", mobilenum);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@joindate", joinyear);
            command.Parameters.AddWithValue("@bonus", bonus);
            command.Parameters.AddWithValue("@adv", adv);
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
        }
        
        public void updateemployee(int id, string cnic, string fn, string ln, int gender, int emptype, int sal, string mobilenum
                                 , string address, string joinyear, int bonus,int adv)
        {
            
            using (SqlCommand command = new SqlCommand("[updateemployee]", MyConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@cnic", cnic);
                command.Parameters.AddWithValue("@finame", fn);
                command.Parameters.AddWithValue("@laname", ln);
                command.Parameters.AddWithValue("@gen", gender);
                command.Parameters.AddWithValue("@emptype", emptype);
                command.Parameters.AddWithValue("@salary", sal);
                command.Parameters.AddWithValue("@mobilenum", mobilenum);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@joindate", joinyear);
                command.Parameters.AddWithValue("@bonus", bonus);
                command.Parameters.AddWithValue("@adv", adv);
                MessageBox.Show("Record Updated");
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

            }
        }
        public void updatetutionfee(int tf)
        {
            using (SqlCommand command = new SqlCommand("[updatetutionfee]", MyConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@tutionfee", tf);
                MessageBox.Show("Tutionfee updated successfully");
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
            }
        }

        public void updatesecuritydeposit(int sd)
        {
            using (SqlCommand command = new SqlCommand("[updatesecuritydeposit]", MyConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@securitydeposit", sd);
                MessageBox.Show("Securitydeposit updated successfully");
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
            }
        }
      public void fee(int id, string fin, string min, string lan, int clas, int sec, string addres,string cnic, string duedate, string currentdate, string month, int amount)
        {
            command = new SqlCommand("[fees]", MyConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@stuid", id);
            command.Parameters.AddWithValue("@finame", fin);
            command.Parameters.AddWithValue("@miname", min);
            command.Parameters.AddWithValue("@laname", lan);
            command.Parameters.AddWithValue("@clas", clas);
            command.Parameters.AddWithValue("@sec", sec);
            command.Parameters.AddWithValue("@addres", addres);
            command.Parameters.AddWithValue("@cnic", cnic);
            command.Parameters.AddWithValue("@duedat", duedate);
            command.Parameters.AddWithValue("@currentdat", currentdate);
            command.Parameters.AddWithValue("@mon", month);
            command.Parameters.AddWithValue("@amount", amount);
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
        }
        
       
    }

}
