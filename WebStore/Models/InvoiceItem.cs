using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
