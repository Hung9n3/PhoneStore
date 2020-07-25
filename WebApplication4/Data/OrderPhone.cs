using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data
{
    public class OrderPhone
    {
        
        public int OrderId { get; set; }
        public int PhoneId { get; set; }
        public Order Order { get; set; }
        public Phone Phone { get; set; }
    }
}
