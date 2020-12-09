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
    public class DriverController : Controller
    {
        // GET: Driver
        public ActionResult DriverApplication()
        {
            DriverApplicationModel viewModel = new DriverApplicationModel();
            return View(viewModel);
        }

        public ActionResult submitDriverApplication(DriverApplicationModel viewModel)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO End_User (Name,Phone,Password) VALUES ('"+viewModel.Name+"','"+viewModel.Phone+"','"+viewModel.Password+ "');";

                sql += "INSERT INTO Driver (Availability, User_id) VALUES ('" + viewModel.Availablility + "',(SELECT SCOPE_IDENTITY()));";

                sql += "INSERT INTO Driver_Application (Driver_id, Driver_record, Driver_license, Short_answer, Driver_Decision)"
                            + " VALUES ((SELECT SCOPE_IDENTITY()),'" + viewModel.Driver_license + "','" + viewModel.Driver_record + "','" + viewModel.Short_answer + "','false');";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return RedirectToAction("SuccessPage", "Home");
                else
                    return RedirectToAction("ErrorPage", "Home");
            }
        }

        [HttpPost]
        public ActionResult submitDriverApplicationData(DriverApplicationModel viewModel)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO End_User (Name,Phone,Password) VALUES ('" + viewModel.Name + "','" + viewModel.Phone + "','" + viewModel.Password + "');";

                sql += "INSERT INTO Driver (Availability, User_id) VALUES ('" + viewModel.Availablility + "',(SELECT SCOPE_IDENTITY()));";

                sql += "INSERT INTO Driver_Application (Driver_id, Driver_record, Driver_license, Short_answer, Driver_Decision)"
                            + " VALUES ((SELECT SCOPE_IDENTITY()),'" + viewModel.Driver_license + "','" + viewModel.Driver_record + "','" + viewModel.Short_answer + "','false');";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (result > 0)
                    return Json("Successfully submitted the driver application!");
                else
                    return Json("Error on submitting the driver application!");
            }
        }

        public ActionResult AvailableOrders()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = @"SELECT *, d.address as temp1 FROM [Order] a INNER JOIN [Restaurant] b on a.Restaurant_id = b.Restaurant_id 
                            INNER JOIN end_user c ON b.User_id = c.User_id 
                            INNER JOIN Customer d ON d.Customer_id = a.Customer_id
                            where Delivery_status = 'Order Placed';";
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
                                Time_placed = DateTime.Parse(sdr["Time_placed"].ToString()),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Address = sdr["temp1"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
            }
        }

        public ActionResult AvailableOrdersData()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = @"SELECT *, d.address as temp1 FROM [Order] a INNER JOIN [Restaurant] b on a.Restaurant_id = b.Restaurant_id 
                            INNER JOIN end_user c ON b.User_id = c.User_id 
                            INNER JOIN Customer d ON d.Customer_id = a.Customer_id
                            where Delivery_status = 'Order Placed';";
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
                                Time_placed = DateTime.Parse(sdr["Time_placed"].ToString()),
                                Order_id = Convert.ToInt32(sdr["Order_id"]),
                                Address = sdr["temp1"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }

                return Json(result,JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AssignDriver(int orderId)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = @"UPDATE [dbo].[Order]
                                SET [Driver_id] = 2,
                                [Delivery_status] = 'On the way'
                                WHERE Order_id = "+ orderId + ";";
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