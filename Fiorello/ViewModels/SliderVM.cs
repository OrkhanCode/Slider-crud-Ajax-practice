using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class SliderVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderDetail SliderDetail { get; set; }
    }
}
