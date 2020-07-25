using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace WebApplication4.Data.UserModel
{
    public class User : IdentityUser
    {   

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string Role { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
