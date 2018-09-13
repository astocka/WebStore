using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
