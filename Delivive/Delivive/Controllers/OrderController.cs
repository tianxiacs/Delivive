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
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderDetail(int orderId)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderDetailModel> result = new List<OrderDetailModel>();
                string sql = @"SELECT * FROM [Order_Detail] a INNER JOIN [Food] b on a.Restaurant_id = b.Restaurant_id 
                            AND a.Food_id = b.Food_id WHERE order_id = "+orderId+";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            result.Add(new OrderDetailModel
                            {
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Food_name = sdr["Food_name"].ToString(),
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                                Price = Convert.ToInt32(sdr["Price"]),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
            }
        }

        // Return Json for api call
        public ActionResult OrderDetailData(int orderId)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderDetailModel> result = new List<OrderDetailModel>();
                string sql = @"SELECT * FROM [Order_Detail] a INNER JOIN [Food] b on a.Restaurant_id = b.Restaurant_id 
                            AND a.Food_id = b.Food_id WHERE order_id = " + orderId + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            result.Add(new OrderDetailModel
                            {
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Food_name = sdr["Food_name"].ToString(),
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                                Price = Convert.ToInt32(sdr["Price"]),
                            });
                        }
                    }
                    con.Close();
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AcceptOrder(int orderId)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = @"UPDATE [dbo].[Order]
                                SET [Driver_id] = 2,
                                [Delivery_status] = 'Order Accepted'
                                WHERE Order_id = " + orderId + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("SuccessPage", "Home");
            }

            return RedirectToAction("SuccessPage", "Home");
        }

        public ActionResult DeclineOrder(int orderId)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = @"UPDATE [dbo].[Order]
                                SET [Driver_id] = 2,
                                [Delivery_status] = 'Order Declined'
                                WHERE Order_id = " + orderId + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("SuccessPage", "Home");
            }

            return RedirectToAction("SuccessPage", "Home");
        }
    }
}