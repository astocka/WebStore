using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
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
        [Required]
        public string Email { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaymentDue { get; set; }

        public IList<InvoiceItem> Items { get; set; }

        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }

        public decimal Total
        {
            get
            {
                return Items.Sum(l => l.Total);
            }
        }
    }
}
