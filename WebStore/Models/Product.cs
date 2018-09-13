using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

    }
}
