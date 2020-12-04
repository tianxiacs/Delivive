using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Delivive.Models
{
    public class RestaurantApplicationModel : RestaurantModel
    {
        public bool Restaurant_id { get; set; }
        [Display(Name = "Small local?")]
        public bool Small_local { get; set; }
        [Display(Name = "Business license?")]
        public bool Business_license { get; set; }
        public string Short_answer { get; set; }
        public int? Admin_id { get; set; }
        public bool Driver_Decision { get; set; }
    }
}