using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Ticketing_System.Models;
using System.Configuration;

namespace Ticketing_System.Business_Logic
{
    public class BLL_User
    {

        readonly string connectionStr = ConfigurationManager.ConnectionStrings["TicketingSystemConnection"].ConnectionString;

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
                            u.DOB = reader["DOB"].ToString();
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

        public User GetUseByUsername(string username)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from tbl_UserDetails where Username = @Username";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Username", username);

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new User();
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.FullName = reader["FullName"].ToString();
                            user.DOB = reader["DOB"].ToString();
                            user.Username = reader["Username"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.Confirm_Password = reader["Confirm_Password"].ToString();

                        }
                    }

                    return user;
                }
            }

        }

        public bool CheckLogin()
        {
            string username = HttpContext.Current.Session["username"] as string;
            if (username == null)
                return false;

            var user = GetUseByUsername(username);
            if (user != null)
                return true;

            return false;
        }
    }
}