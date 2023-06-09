using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<int> GetCountAsync() => await _context.Sliders.Where(m => !m.SoftDelete && m.Status).CountAsync();

        public async Task CreateAsync(List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "img");

                Slider slider = new()
                {
                    Image = fileName
                };

                await _context.Sliders.AddAsync(slider);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await GetByIdAsync(id);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(Slider slider, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "img");

            slider.Image = fileName;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAllAsync() => await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<List<Slider>> GetAllByStatusAsync() => await _context.Sliders.Where(m => !m.SoftDelete && m.Status).ToListAsync();

        public async Task<List<SliderVM>> GetAllMappedDatasAsync()
        {
            IEnumerable<Slider> sliders = await GetAllAsync();

            List<SliderVM> model = new List<SliderVM>();

            foreach (var item in sliders)
            {
                SliderVM sliderVM = new()
                {
                    Id = item.Id,
                    Image = item.Image,
                    CreateDate = item.CreatedDate,
                    Status = item.Status
                };

                model.Add(sliderVM);
            }

            return model;
        }

        public async Task<Slider> GetByIdAsync(int id) => await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        public SliderDetailVM GetMappedData(Slider slider)
        {
            SliderDetailVM model = new()
            {
                Image = slider.Image,
                CreateDate = slider.CreatedDate.ToString("dd/MM/yyyy"),
                Status = slider.Status
            };

            return model;
        }

        public async Task<bool> ChangeStatusAsync(Slider slider)
        {
            if (slider.Status && await GetCountAsync() != 1)
            {
                slider.Status = false;
            }
            else
            {
                slider.Status = true;
            }

            await _context.SaveChangesAsync();

            return slider.Status;
        }
    }
}