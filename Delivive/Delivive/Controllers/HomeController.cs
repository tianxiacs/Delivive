using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Delivive.Models;
using System.Web.Script.Serialization;

namespace Delivive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<RestaurantViewModel> result = GetRestaurants();
            ViewBag.Result = result;
            return View();
        }

        public List<RestaurantViewModel> GetRestaurants()
        {
            string constr = @"data source=SVRDEV\SQL2016DEV;initial catalog=Delivive;persist security info=True;user id=sa;password=Origin@lW@vyChips2100$";
            List<RestaurantViewModel> resturants = new List<RestaurantViewModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Restaurant";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            resturants.Add(new RestaurantViewModel
                            {
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Business_hour = sdr["Business_hour"].ToString(),
                                Address = sdr["Address"].ToString(),
                                User_id = Convert.ToInt32(sdr["User_id"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }
            }

            return resturants;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}