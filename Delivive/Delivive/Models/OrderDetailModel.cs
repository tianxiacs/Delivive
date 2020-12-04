using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class OrderDetailModel : FoodModel
    {
        public int Order_id { get; set; }
        public int Restaurant_id { get; set; }
        public int Food_id { get; set; }
        public int Quantity { get; set; }
    }
}