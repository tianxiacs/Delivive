using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Delivive.Models
{
    public class DriverApplicationModel : DriverModel
    {
        [Display(Name = "Driver record")]
        public bool Driver_record { get; set; }
        public bool Driver_license { get; set; }
        public string Short_answer { get; set; }
        public int? Admin_id { get; set; }
        public bool Driver_Decision { get; set; }
    }
}