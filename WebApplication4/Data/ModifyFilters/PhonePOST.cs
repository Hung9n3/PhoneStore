using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data.ModifyFilters
{
    public class PhonePOST
    {
        public string PhoneName { get; set; }
        public int Price { get; set; }
        public int OSID { get; set; }
        public int Battery { get; set; }
        public double BatteryVolume { get; set; }
    }
}
