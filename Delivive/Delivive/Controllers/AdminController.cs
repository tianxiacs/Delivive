using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivive.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Delivive.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApproveDriverData(int adminId, int driverId, int appId)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "UPDATE [dbo].[Driver_Application] " +
                        " SET [Driver_Decision] = true " +
                        " WHERE Driver_id = " + driverId + " AND App_id = " + appId + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully approve application!", JsonRequestBehavior.AllowGet);
                else
                    return Json("Error on approve application!");
            }
        } 
        
        public ActionResult ApproveRestaurantData(int adminId, int RestaurantId, int appId)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "UPDATE [dbo].[Restaurant_Application] " +
                        " SET [Driver_Decision] = true " +
                        " WHERE Restaurant_id = " + RestaurantId + " AND App_id = " + appId + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully approve application!");
                else
                    return Json("Error on approve application!");
            }
        }
    }
}