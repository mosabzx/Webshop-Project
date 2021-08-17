using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        [Required]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        
        [NotMapped]
        [DisplayName("Product Image")]
        public IFormFile ImageFile { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Product Color")]
        public string Color { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
