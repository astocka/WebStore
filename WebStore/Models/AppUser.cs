using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser(string userName) : base(userName)
        {
        }

        public AppUser()
        {
        }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
