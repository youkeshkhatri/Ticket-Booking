using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Ticketing_System.Models;

namespace Ticketing_System.Business_Logic
{
    public class BLL_TicketType
    {
        string strConnection = "Data Source=Youkesh; Initial Catalog=TicketingSystem; Integrated Security= true;";

        public List<TicketType> GetTicketTypes()
        {
            List<TicketType> listTicketType = new List<TicketType>();
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from TicketDetails";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TicketType tt = new TicketType();
                            tt.Id = Convert.ToInt32(reader["Id"]);
                            tt.TicketName = reader["TicketName"].ToString();

                            listTicketType.Add(tt);
                        }
                    }
                    return listTicketType;

                }
            }
        }

        public TicketType GetTicketTypesByID(int id)
        {
            TicketType tt = new TicketType();

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from TicketDetails where Id = @ID";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tt.Id = Convert.ToInt32(reader["Id"]);
                            tt.TicketName = reader["TicketName"].ToString();
                            tt.Price = Convert.ToDecimal(reader["Price"]);

                        }
                    }
                    return tt;

                }
            }
        }

    }
}