using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Rating { get; set; } = 0;
        [Required]
        public string Content { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string UserName { get; set; }
        public AppUser User { get; set; }
    }
}
