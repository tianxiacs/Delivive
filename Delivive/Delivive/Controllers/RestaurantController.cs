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
                string sql = "SELECT * FROM [Restaurant] a INNER JOIN [End_User] b on a.user_id = b.user_id;";
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
                                Name = sdr["Name"].ToString(),
                                Phone = sdr["Phone"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
            }
        }

        public ActionResult GetRestaurantOrders()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                //string sql = "SELECT * FROM [Order]"
                //            + " WHERE Restaurant_id = " + restaurant_id + ";";
                string sql = "SELECT * FROM [Order]"
                            + " WHERE Restaurant_id = " + 1 + ";";
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
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = "SELECT * FROM [Food]"
                            + " WHERE Restaurant_id = " + 1 + ";";
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
                ViewBag.Result = result;
            }

            FoodModel viewModel = new FoodModel();
            //viewModel.Restaurant_id = int.Parse( Session["Restaurant_id"].ToString() );
            viewModel.Restaurant_id = 1;
            return View(viewModel);
        }

        public ActionResult addMenuAction(FoodModel model)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = "INSERT INTO Food ([Restaurant_id],[Food_name],[Price],[Description]) VALUES (" +
                            model.Restaurant_id + ",'" +
                            model.Food_name + "'," +
                            model.Price + ",'" +
                            model.Description + "');";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json("Submit Succesfully!", JsonRequestBehavior.AllowGet);
            }
        }

        // NOT DONE YET
        public ActionResult PlaceOrder(List<FoodModel> foods)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = @"INSERT INTO [ORDER] ([Time_placed]
           ,[Time_delivery]
           ,[Address_id]
           ,[Delivery_status]
           ,[Customer_id]
           ,[Driver_id]
           ,[Restaurant_id]) VALUES ('" +
                        DateTime.Now + "','" +
                          DateTime.Now + "'," +
                        2 + "," +
                            "'Order Placed'" + "," +
                             3 + "," +
                              2 + "," +
                               1 + ");";
                sql += @"  declare @temp int
		   set @temp = (SELECT SCOPE_IDENTITY());";
                foreach (FoodModel food in foods)
                {
                    sql +=
                        @"INSERT INTO [dbo].[Order_Detail]
                       ([Order_id]
                       ,[Restaurant_id]
                       ,[Food_id]
                       ,[Quantity])
                 VALUES
                       ((@temp)," +
                          food.Restaurant_id + "," +
                          food.Food_id + "," +
                          1 + ");";
                }
              


                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Json("Successfully");
            }

            return Json("Successfully");
        }
    }
}