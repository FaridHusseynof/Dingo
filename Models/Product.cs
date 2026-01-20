using System.ComponentModel.DataAnnotations;

namespace Dingo.Models
{
    public class Product:BaseModel
    {
        [Required, Range(0,10000)]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        [Required, MaxLength(60)]
        public string Description { get; set; }
        [Required, Length(3,30)]
        public string Title { get; set; }
    }
}
