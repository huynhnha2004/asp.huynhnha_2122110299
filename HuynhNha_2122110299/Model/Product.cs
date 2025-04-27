


using System.ComponentModel.DataAnnotations;

namespace HuynhNha_2122110299.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
     

        public string? Description { get; set; }


   
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        //public int BrandId { get; set; }
        //public Brand? Brand { get; set; }

        [Required]
        [MaxLength(500)] // Độ dài tối đa cho đường dẫn
        public string ImageUrl { get; set; } = string.Empty;





    }

}
