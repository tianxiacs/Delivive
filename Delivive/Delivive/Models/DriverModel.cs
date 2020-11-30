using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class DriverModel : EndUserModel
    {
        public int Driver_id { get; set; }
        public string Availablility { get; set; }
    }
}