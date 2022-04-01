using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object AddCustomer(welcome wel)
        {
            if (ModelState.IsValid)
            {

                string connectionStr = "Data Source=DESKTOP-Q7GVR21; Initial Catalog=TicketingSystem; Integrated Security= true;";
                SqlConnection connection = new SqlConnection(connectionStr);
                String pname = "proc_CustomerDetails";
                connection.Open();
                SqlCommand com = new SqlCommand(pname, connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Date", wel.Date);
                com.Parameters.AddWithValue("@FullName", wel.FullName);
                com.Parameters.AddWithValue("@TicketType", wel.TicketType);
                com.Parameters.AddWithValue("@Amount", wel.Price);
                com.Parameters.AddWithValue("@TotalPeople", wel.TotalPeople);
                com.Parameters.AddWithValue("@Discount", wel.Discount);
                com.Parameters.AddWithValue("@NetAmount", wel.NetAmount);
                com.Parameters.AddWithValue("@Vat", wel.Vat);
                com.Parameters.AddWithValue("@TotalAmount", wel.TotalAmount);

                int res = com.ExecuteNonQuery();
                connection.Close();
                return res;
            }

            return 0; 

        }



        [HttpGet]
        public List<welcome> GetCustomer()
        {
            string connectionStr = "Data Source=DESKTOP-Q7GVR21; Initial Catalog=TicketingSystem; Integrated Security= true;";
            List<welcome> list = new List<welcome>();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from tbl_CustomerDetails";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            welcome w = new welcome();
                            w.Date = Convert.ToDateTime(reader["Date"]);
                            w.FullName = reader["FullName"].ToString();
                            w.TicketType = reader["TicketType"].ToString();
                            w.Price = Convert.ToInt32(reader["Amount"]);
                            w.TotalPeople = Convert.ToInt32(reader["TotalPeople"]);
                            w.Discount = Convert.ToInt32(reader["Discount"]);
                            w.NetAmount = Convert.ToInt32(reader["NetAmount"]);
                            w.Vat = Convert.ToInt32(reader["Vat"]);
                            w.TotalAmount = Convert.ToInt32(reader["TotalAmount"]);
                            
                            list.Add(w);

                        }
                        
                    }

                    return list;
                }
            }

        }


        public JsonResult GetCustomerList ()
        {
            return Json(GetCustomer(), JsonRequestBehavior.AllowGet);
        }

    }
    
}