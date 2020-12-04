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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(CustomerModel viewModel)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO End_User (Name,Phone,Password) VALUES ('" + viewModel.Name + "','" + viewModel.Phone + "','" + viewModel.Password + "');";
                int result = 0;
                sql += "INSERT INTO Customer (Address, User_id) VALUES ('" + viewModel.Address + "',(SELECT SCOPE_IDENTITY()));";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if(result > 0)
                    return RedirectToAction("SuccessPage", "Home");
                else
                    return RedirectToAction("ErrorPage", "Home");
            }
            return RedirectToAction("SuccessPage", "Home");
        }

        public ActionResult GetCustomerOrders()
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                //string sql = "SELECT * FROM [Order]"
                //            + " WHERE Customer_id = " + customer_id + ";";
                string sql = @"SELECT *, d.address as cust_Addr FROM [Order] a INNER JOIN [Restaurant] b on a.Restaurant_id = b.Restaurant_id
                            INNER JOIN end_user c ON b.User_id = c.User_id
                            INNER JOIN Customer d ON d.Customer_id = a.Customer_id
                            WHERE a.Customer_id = " + Session["Customer_id"].ToString() + ";";
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

                return View(result);
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MakeCustomerOrder(int customer_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = "INSERT INTO ORDER (Customer_id,Delivery_status) VALUES ("
                            + "(" + Session["Customer_id"].ToString() + ","+"Order Placed"+")";
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
                                Time_placed = DateTime.Parse(sdr["Time_placed"].ToString()),
                                Time_delivery = DateTime.Parse(sdr["Time_delivery"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }

                return View(result);
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}