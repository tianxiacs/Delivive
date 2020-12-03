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

        public ActionResult GetCustomerOrders(int customer_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = "SELECT * FROM [Order]"
                            + " WHERE Customer_id = " + customer_id + ";";
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

        public ActionResult MakeCustomerOrder(int customer_id)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                List<OrderModel> result = new List<OrderModel>();
                string sql = "INSERT INTO ORDER (Customer_id,Address_id,Delivery_status) VALUES ("
                            + "(" + Session["UserId"] + ","+"1"+","+"Order Placed"+")";
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