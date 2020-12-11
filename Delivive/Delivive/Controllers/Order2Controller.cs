using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Delivive.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Delivive.Controllers
{
    public class Order2Controller : ApiController
    {
        public IEnumerable<OrderModel> Get(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = "SELECT * FROM [dbo].[Order] WHERE Order_Id = " + id + ";";
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
                                Time_placed = Convert.ToDateTime(sdr["Time_placed"]),
                                Time_delivery = Convert.ToDateTime(sdr["Time_delivery"]),
                                Rating = Convert.ToInt32(sdr["Rating"]),
                                Comment = sdr["Comment"].ToString(),
                                Delivery_status = sdr["Delivery_status"].ToString(),
                                Customer_id = Convert.ToInt32(sdr["Customer_id"]),
                                Driver_id = Convert.ToInt32(sdr["Driver_id"]),
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                            });
                        }
                    }
                    con.Close();
                }

                return result;

            }
        }

        // POST: api/OrderDetail2
        public void Post(OrderModel view)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO [dbo].[Order] ([Time_placed],[Time_delivery],[Rating],[Comment],[Delivery_status],[Customer_id],[Driver_id],[Restaurant_id]) VALUES ('"
                            + view.Time_placed + "','" + view.Time_delivery + "'," + view.Rating + ",'" + view.Comment+ "','" + view.Delivery_status + "'," +
                            view.Customer_id + "," + view.Driver_id + "," + view.Restaurant_id + ");";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                //if (result > 0)
                //    return Json("Successfully inserted!");
                //else
                //    return Json("Error on inserting!");
            }
        }

        // PUT: api/OrderDetail2/5
        public void Put(OrderModel view)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "UPDATE [dbo].[Order] " +
                                  " SET [Time_placed] = " + view.Time_placed +
                                  ",[Time_delivery] = " + view.Time_delivery +
                                  ",[Rating] = " + view.Rating +
                                  ",[Comment] = " + view.Comment +
                                  ",[Delivery_status] = " + view.Delivery_status +
                                  ",[Customer_id] = " + view.Customer_id +
                                  ",[Driver_id] = " + view.Driver_id +
                                  ",[Restaurant_id] = " + view.Restaurant_id +
                                  " WHERE Order_id = " + view.Order_id + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                //if (result > 0)
                //    return Json("Successfully Updated Order_Detail!", JsonRequestBehavior.AllowGet);
                //else
                //    return Json("Error on Updateing Order_Detail!");
            }
        }

        // DELETE: api/OrderDetail2/5
        public void Delete(int id)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "DELETE FROM [dbo].[Order] " +
                        " WHERE Order_id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                //if (result > 0)
                //    return Json("Successfully Deleted!");
                //else
                //    return Json("Error on Deleting!");
            }
        }
    }
}
