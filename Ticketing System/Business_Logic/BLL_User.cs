using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Ticketing_System.Models;

namespace Ticketing_System.Business_Logic
{
    public class BLL_User
    {

        string connectionStr = "Data Source=DESKTOP-Q7GVR21; Initial Catalog=TicketingSystem; Integrated Security= true;";

        public int AddUser(User user)
        {

            SqlConnection connection = new SqlConnection(connectionStr);
            String pname = "proc_InsertUserDetails";
            connection.Open();
            SqlCommand com = new SqlCommand(pname, connection);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@FullName", user.FullName);
            com.Parameters.AddWithValue("@DOB", user.DOB);
            com.Parameters.AddWithValue("@Username", user.Username);
            com.Parameters.AddWithValue("@Password", user.Password);
            com.Parameters.AddWithValue("@Confirm_Password", user.Confirm_Password);

            int res = com.ExecuteNonQuery();
            connection.Close();
            return res;
        }


        public List<User> GetUser()
        {
            List<User> user = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from tbl_UserDetails";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User u = new User();
                            u.Id = Convert.ToInt32(reader["Id"]);
                            u.FullName = reader["FullName"].ToString();
                            u.DOB = Convert.ToDateTime(reader["DOB"].ToString());
                            u.Username = reader["Username"].ToString();
                            u.Password = reader["Password"].ToString();
                            u.Confirm_Password = reader["Confirm_Password"].ToString();

                            user.Add(u);

                        }
                    }

                    return user;
                }
            }

        }
    }
}