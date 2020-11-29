using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivive.Models;

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
            string sql = "INSERT INTO Driver_Application (Driver_id, App_id, Driver_record, Driver_license, Short_answer, Admin_id, Driver_Decision)"
                            + " VALUES ('" + viewModel.Driver_license + "','" + viewModel.Driver_record + "')";
            return View(viewModel);
        }
    }
}