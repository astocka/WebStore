using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
