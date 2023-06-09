using Microsoft.Build.Framework;

namespace Fiorello.Areas.Admin.ViewModels.SliderInfo
{
    public class SliderInfoCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile SignImage { get; set; }
    }
}
