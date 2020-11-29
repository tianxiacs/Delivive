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
            DriverApplicationViewModel viewModel = new DriverApplicationViewModel();
            return View(viewModel);
        }

        public ActionResult submitDriverApplication(DriverApplicationViewModel viewModel)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeliviveConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string sql = "INSERT INTO Driver_Application (Driver_record, Driver_license, Short_answer, Driver_Decision)"
                            + " VALUES ('" + viewModel.Driver_license + "','" + viewModel.Driver_record + "','" + viewModel.Short_answer + "','false')";
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
    }
}