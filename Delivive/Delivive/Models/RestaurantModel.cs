using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class RestaurantModel : EndUserModel
    {
        public int Restaurant_id { get; set; }

        public string Business_hour { get; set; }

        public string Address { get; set; }

        public int User_id { get; set; }
    }
}