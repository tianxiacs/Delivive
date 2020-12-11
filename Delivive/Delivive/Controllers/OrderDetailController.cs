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
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        [HttpGet]
        public ActionResult Get(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderDetailModel> result = new List<OrderDetailModel>();
                string sql = "SELECT * FROM [dbo].[Order_Detail] WHERE OrderDetailId = " + id + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            DateTime? deliveryTime = null;
                            if (sdr["Time_delivery"] != DBNull.Value)
                            {
                                deliveryTime = DateTime.Parse(sdr["Time_delivery"].ToString());
                            }
                            result.Add(new OrderDetailModel
                            {
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Restaurant_id = Convert.ToInt32(sdr["Restaurant_id"]),
                                Food_id = Convert.ToInt32(sdr["Food_id"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                            });
                        }
                    }
                    con.Close();
                }

                return Json(result);

            }
        }

            [HttpPut]
        public ActionResult Put(OrderDetailModel view)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "UPDATE [dbo].[Order_Detail] " +
                                " SET [Order_id] = " + view.Order_id +
                                ",[Restaurant_id] = " + view.Restaurant_id +
                                ",[Food_id] = " + view.Food_id +
                                ",[Quantity] = " + view.Quantity + ";";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully Updated Order_Detail!", JsonRequestBehavior.AllowGet);
                else
                    return Json("Error on Updateing Order_Detail!");
            }
        }

        [HttpPost]
        public ActionResult Post(OrderDetailModel view)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO [dbo].[Order_Detail] ([Order_id],[Restaurant_id,[Food_id,[Quantity]) VALUES ("
                            + view.Order_id + "," + view.Restaurant_id + "," + view.Food_id + "," + view.Quantity +");";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully inserted!");
                else
                    return Json("Error on inserting!");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int OrderDetailId)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "DELETE FROM [dbo].[Order_Detail] " +
                        " WHERE " +
                        " WHERE OrderDetail_id = " + OrderDetailId;
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully Deleted!");
                else
                    return Json("Error on Deleting!");
            }
        }
    }
}