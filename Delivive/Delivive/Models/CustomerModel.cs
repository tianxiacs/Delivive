using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivive.Models
{
    public class CustomerModel : EndUserModel
    {
        public int Customer_id { get; set; }
        public string Address { get; set; }
        public string ConfirmPassword { get; set; }
    }
}