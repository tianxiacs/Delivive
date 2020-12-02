using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class FoodModel
    {
        public int Restaurant_id { get; set; }
        public int Food_id { get; set; }
        public string Food_name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}