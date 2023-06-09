using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<List<SliderVM>> GetAllMappedDatasAsync();
        SliderDetailVM GetMappedData(Slider slider);
        Task CreateAsync(List<IFormFile> images);
        Task DeleteAsync(int id);
        Task EditAsync(Slider slider, IFormFile newImage);
        Task<List<Slider>> GetAllByStatusAsync();
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
    }
}