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
            RestaurantApplicationModel viewModel = new RestaurantApplicationModel();
            return View(viewModel);
        }

        public ActionResult submitRestaurantApplication(RestaurantApplicationModel viewModel)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO End_User (Name,Phone,Password) VALUES ('" + viewModel.Name + "','" + viewModel.Phone + "','" + viewModel.Password + "');";

                sql += "INSERT INTO Restaurant (Business_hour, Address, User_id) VALUES ('" + viewModel.Business_hour + "' , '" + viewModel.Address + "',(SELECT SCOPE_IDENTITY()));";

                sql += "INSERT INTO Restaurant_Application (Restaurant_id, [Small_local], [Business_license], Short_answer, Restaurant_Decision)"
                            + " VALUES ((SELECT SCOPE_IDENTITY()),'" + viewModel.Small_local + "','" + viewModel.Business_license + "','" + viewModel.Short_answer + "','false');";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result= cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return RedirectToAction("SuccessPage", "Home");
                else
                    return RedirectToAction("ErrorPage", "Home");
            }
        }

        // Return Json for api call
        [HttpPost]
        public ActionResult submitRestaurantApplicationData(RestaurantApplicationModel viewModel)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO End_User (Name,Phone,Password) VALUES ('" + viewModel.Name + "','" + viewModel.Phone + "','" + viewModel.Password + "');";

                sql += "INSERT INTO Restaurant (Business_hour, Address, User_id) VALUES ('" + viewModel.Business_hour + "' , '" + viewModel.Address + "',(SELECT SCOPE_IDENTITY()));";

                sql += "INSERT INTO Restaurant_Application (Restaurant_id, [Small_local], [Business_license], Short_answer, Restaurant_Decision)"
                            + " VALUES ((SELECT SCOPE_IDENTITY()),'" + viewModel.Small_local + "','" + viewModel.Business_license + "','" + viewModel.Short_answer + "','false');";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully submitted the restaurant application!");
                else
                    return Json("Error on submitting the restaurant application!");
            }
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

        // Return Json for api call
        public ActionResult GetRestaurantsData()
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

                return Json(result, JsonRequestBehavior.AllowGet);
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
                string sql = @"SELECT *, d.address as cust_Addr FROM [Order] a INNER JOIN [Restaurant] b on a.Restaurant_id = b.Restaurant_id
                            INNER JOIN end_user c ON b.User_id = c.User_id 
                            INNER JOIN Customer d ON d.Customer_id = a.Customer_id 
                            WHERE a.Restaurant_id = " + Session["Restaurant_id"].ToString() + ";";
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
                                Name = sdr["Name"].ToString(),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Time_placed = DateTime.Parse(sdr["Time_placed"].ToString()),
                                Time_delivery = DateTime.Parse(sdr["Time_delivery"].ToString()),
                                Address = sdr["cust_Addr"].ToString(),
                                Delivery_status = sdr["Delivery_status"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        // Return Json for api call
        public ActionResult GetRestaurantOrdersData(int Restaurant_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                //string sql = "SELECT * FROM [Order]"
                //            + " WHERE Restaurant_id = " + restaurant_id + ";";
                string sql = @"SELECT *, d.address as cust_Addr FROM [Order] a INNER JOIN [Restaurant] b on a.Restaurant_id = b.Restaurant_id
                            INNER JOIN end_user c ON b.User_id = c.User_id 
                            INNER JOIN Customer d ON d.Customer_id = a.Customer_id 
                            WHERE a.Restaurant_id = " + Restaurant_id + ";";
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
                                Name = sdr["Name"].ToString(),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Time_placed = DateTime.Parse(sdr["Time_placed"].ToString()),
                                Time_delivery = DateTime.Parse(sdr["Time_delivery"].ToString()),
                                Address = sdr["cust_Addr"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                //return View(result);
                return Json(result, JsonRequestBehavior.AllowGet);
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

        // Return Json for api call
        public ActionResult GetRestaurantMenuData(int restaurant_id)
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

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddMenu()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = "SELECT * FROM [Food]"
                            + " WHERE Restaurant_id = " + Session["Restaurant_id"].ToString() + ";";
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
            viewModel.Restaurant_id = Convert.ToInt32(Session["Restaurant_id"]);
            return View(viewModel);
        }

        public ActionResult addMenuAction(FoodModel model)
        {
            int result2 = 0;
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
                    result2 = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result2 > 0)
                    return RedirectToAction("SuccessPage", "Home");
                else
                    return RedirectToAction("ErrorPage", "Home");
            }
        }

        // Return Json for api call
        [HttpPost]
        public ActionResult addMenuActionData(FoodModel model)
        {
            int result2 = 0;
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
                    result2 = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result2 > 0)
                    return Json("Successfully added to the menu!");
                else
                    return Json("Error on adding to the menu!");
            }
        }

        // NOT DONE YET
        public ActionResult PlaceOrder(List<FoodModel> foods)
        {
            int result2 = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<FoodModel> result = new List<FoodModel>();
                string sql = @"INSERT INTO [ORDER] ([Time_placed]
           ,[Time_delivery]
           ,[Delivery_status]
           ,[Customer_id]
           ,[Restaurant_id]) VALUES ('" +
                        DateTime.Now + "','" +
                          DateTime.Now + "'," +
                            "'Order Placed'" + "," +
                             Session["Customer_id"].ToString() + "," +
                               foods[0].Restaurant_id + ");";
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
                    result2 = cmd.ExecuteNonQuery();
                    con.Close();
                }


                if (result2 > 0)
                    return Json("Success");
                else
                    return Json("Fail");
            }

            return Json("Successfully");
        }
    }
}