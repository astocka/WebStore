using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserName { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
