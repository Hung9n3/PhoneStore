using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication4.Data
{
    public class OS
    {
        [Key]
        public int OSID { get; set; }
        [Required]
        public string OsName { get; set; }
    }
}
