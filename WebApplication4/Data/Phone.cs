using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Data
{
    public class Phone
    {
        [Key]
        public int PhoneId { get; set; }
        [Required]
        public string PhoneName { get; set; }
        [Required]
        public int? Price { get; set; }
        
        [ForeignKey("OSID")]
        public int? OSID { get; set; }
        public Battery PhoneBattery { get; set; }
        public  List<OrderPhone> OrderPhones { get; set; }
      
    }
}
