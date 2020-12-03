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
        public ActionResult RestaurantApplication()
        {
            return View();
        }

        public ActionResult GetRestaurants()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<RestaurantModel> result = new List<RestaurantModel>();
                string sql = "SELECT * FROM [Restaurant];";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            result.Add(new RestaurantModel
                            {
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Business_hour = sdr["Business_hour"].ToString(),
                                Address = sdr["Address"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
            }
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

        public ActionResult GetRestaurantMenu(int restaurant_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = "SELECT * FROM [Food]"
                            + " WHERE Restaurant_id = " + restaurant_id + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            result.Add(new FoodModel
                            {
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Food_id = Convert.ToInt32(sdr["Food_id"]),
                                Food_name = sdr["Food_name"].ToString(),
                                Price = Convert.ToInt32(sdr["Price"]),
                                Description = sdr["Description"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
            }
        }

        public ActionResult AddMenu()
        {
            FoodModel viewModel = new FoodModel();
            viewModel.Restaurant_id = int.Parse( Session["Restaurant_id"].ToString() );
            return View(viewModel);
        }
    }
}