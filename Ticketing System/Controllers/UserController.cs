using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Business_Logic;
using Ticketing_System.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Ticketing_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public object AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                BLL_User bLL_User = new BLL_User();
                int res = bLL_User.AddUser(user);
                return JsonConvert.SerializeObject(res);
            }
            return JsonConvert.SerializeObject(0);
        }


        [HttpGet]
        public JsonResult GetUser()
        {
            BLL_User bLL_User = new BLL_User();
            List<User> listUser = bLL_User.GetUser();

            return Json(listUser, JsonRequestBehavior.AllowGet);
        }


        //For LOGIN

        [HttpPost]
        public ActionResult Index(LoginClass loginClass)
        {
            string connectionStr = "Data Source=Youkesh; Initial Catalog=TicketingSystem; Integrated Security= true;";
            SqlConnection sqlconn = new SqlConnection(connectionStr);
            string sqlquery = "select Username, Password, IsLoggedIn from dbo.tbl_UserDetails where Username = @Username and Password = @Password";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Username", loginClass.UserName);
            sqlcomm.Parameters.AddWithValue("@Password", loginClass.Password);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if(sdr.Read())
            {
                Session["username"] = loginClass.UserName.ToString();
                return RedirectToAction("Index", "Welcome");
            }
            else
            {
                ViewData["Message"] = "Login Failed !";
            }
            sqlconn.Close();
            return View();
        }


        /*
         
          if (sdr.Read())
            {
                Session["username"] = loginClass.UserName.ToString();
                if (loginClass.IsLoggedIn)
                {
                    sqlquery = "Update dbo.tbl_UserDetails set IsLoggedIn = 1 where Username = @Username and Password = @Password";
                    sqlcomm.ExecuteReader();
                    return RedirectToAction("Welcome");
                }

                else
                    return RedirectToAction("Index");
            }
         */

        //FOR TICKET DROPDOWN

        BLL_TicketType bLL_TicketType = null;
        public UserController()
        {
            bLL_TicketType = new BLL_TicketType();
        }


        [HttpGet]
        public JsonResult GetAll()
        {
            List<TicketType> list = bLL_TicketType.GetTicketTypes();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTicketDetails(int id)
        {
            TicketType list = bLL_TicketType.GetTicketTypesByID(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}