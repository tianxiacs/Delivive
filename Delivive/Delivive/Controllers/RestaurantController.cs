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
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRestaurantOrders(int restaurant_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = "SELECT * FROM [Order]"
                            + " WHERE Restaurant_id = " + restaurant_id + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            result.Add(new OrderModel
                            {
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                //Business_hour = sdr["Business_hour"].ToString(),
                                //Address = sdr["Address"].ToString(),
                                //User_id = Convert.ToInt32(sdr["User_id"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }

                ViewBag.Result = result;
                return View();
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}