using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
