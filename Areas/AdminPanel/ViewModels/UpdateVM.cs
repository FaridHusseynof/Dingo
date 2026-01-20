using System.ComponentModel.DataAnnotations;

namespace Dingo.Areas.AdminPanel.ViewModels
{
    public class UpdateVM
    {
        [Required, Range(0, 10000)]
        public decimal price { get; set; }
        public string? imageURL { get; set; }
        [Required, MaxLength(60)]
        public string description { get; set; }
        [Required, Length(3, 30)]
        public string title { get; set; }
        public int id_ { get; set; }
        public IFormFile? imageFile { get; set; }
    }
}
