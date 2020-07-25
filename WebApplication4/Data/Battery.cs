using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data
{
    public class Battery
    {
        [Key]
        public int BatteryID { get; set; }
        
        public double? Volume { get; set; }
    }
}
