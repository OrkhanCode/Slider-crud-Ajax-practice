using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderDetail SliderDetail { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public About About { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public BlogHeader BlogHeader { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Instagram> Instagram { get; set; }
    }
}
