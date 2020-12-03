using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class OrderModel
    {
        public int Order_id { get; set; }
        public DateTime Time_placed { get; set; }
        public DateTime Time_delivery { get; set; }
    }
}