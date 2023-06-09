using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface ISliderDetailService
    {
        Task<List<SliderDetail>> GetAllDatasAsync();
        Task<SliderDetail> GetByIdAsync(int? id);
        Task DeleteAsync(SliderDetail data);
        Task CreateAsync(string title, string description, IFormFile image);
        Task EditAsync(SliderDetail sliderDetail, IFormFile newSignImage, string title, string description);
    }
}
