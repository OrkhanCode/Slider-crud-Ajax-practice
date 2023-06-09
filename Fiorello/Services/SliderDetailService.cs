using Fiorello.Areas.Admin.ViewModels.SliderInfo;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class SliderDetailService : ISliderDetailService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderDetailService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(string title, string description, IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;

            await image.SaveFileAsync(fileName, _env.WebRootPath, "img");

            SliderDetail sliderDetail = new()
            {
                Title = title,
                Description = description,
                SignImage = fileName
            };

            await _context.SliderDetails.AddAsync(sliderDetail);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SliderDetail data)
        {
            _context.SliderDetails.Remove(data);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", data.SignImage);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderDetail sliderDetail, IFormFile newSignImage, string title, string description)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "img", sliderDetail.SignImage);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid() + "_" + newSignImage.FileName;

            await newSignImage.SaveFileAsync(fileName, _env.WebRootPath, "img");

            sliderDetail.SignImage = fileName;
            sliderDetail.Title = title;
            sliderDetail.Description = description;

            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderDetail>> GetAllDatasAsync() => await _context.SliderDetails.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<SliderDetail> GetByIdAsync(int? id) => await _context.SliderDetails.Where(m => !m.SoftDelete && m.Id == id).FirstOrDefaultAsync();
    }
}
