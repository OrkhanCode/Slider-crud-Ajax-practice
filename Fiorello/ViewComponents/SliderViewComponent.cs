using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public SliderViewComponent(AppDbContext context, ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllByStatusAsync();
            SliderDetail sliderDetail = await _context.SliderDetails.Where(m => !m.SoftDelete).FirstOrDefaultAsync();

            SliderVM model = new()
            {
                Sliders = sliders,
                SliderDetail = sliderDetail
            };

            return await Task.FromResult(View(model));
        }
    }
}
