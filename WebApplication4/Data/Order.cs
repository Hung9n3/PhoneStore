using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data.UserModel;

namespace WebApplication4.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public List<OrderPhone> OrderPhones = new List<OrderPhone>();
        public User Owner { get; set; }
        
    }
}
