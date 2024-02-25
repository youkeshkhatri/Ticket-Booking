using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Ticketing_System.Business_Logic;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    public class TicketBookingController : Controller
    {
        readonly string connectionStr = ConfigurationManager.ConnectionStrings["TicketingSystemConnection"].ConnectionString;

        // GET: Welcome
        public ActionResult Index()
        {
            BLL_User user = new BLL_User();
            var status = user.CheckLogin();
            if (status)
                return View();

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public object AddCustomer(welcome wel)
        {
            if (ModelState.IsValid)
            {

                //string connectionStr = "Data Source=Youkesh; Initial Catalog=TicketingSystem; Integrated Security= true;";
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

        [HttpDelete]
        public string DeleteTicket(int ticketId)
        {
            return "";
        }

        [HttpGet]
        public List<welcome> GetCustomer(string date = null)
        {
            //string connectionStr = "Data Source=Youkesh; Initial Catalog=TicketingSystem; Integrated Security= true;";
            List<welcome> list = new List<welcome>();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (string.IsNullOrEmpty(date))
                        cmd.CommandText = "select * from tbl_CustomerDetails";
                    else
                    {
                        cmd.CommandText = "select * from tbl_CustomerDetails where Date = @date";
                        cmd.Parameters.AddWithValue("@date", date);
                    }

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            welcome w = new welcome();
                            w.Id = Convert.ToInt32(reader["Id"]);
                            w.Date = reader["Date"].ToString();
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

        public JsonResult GetCustomerList()
        {
            return Json(GetCustomer(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerListByDate(string date)
        {
            return Json(GetCustomer(date), JsonRequestBehavior.AllowGet);
        }

    }

}