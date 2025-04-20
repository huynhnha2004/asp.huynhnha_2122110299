using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HuynhNha_2122110299.Request
{
    public class ProductUpdateRequest
    {
        public string? Name { get; set; }

        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        // thêm field để upload ảnh mới
        [Display(Name = "Ảnh thumbnail")]
        public IFormFile? ThumbnailFile { get; set; }


    }
}
