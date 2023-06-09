using Fiorello.Areas.Admin.ViewModels.SliderInfo;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderDetailController : Controller
    {
        private readonly ISliderDetailService _sliderDetailService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderDetailController(ISliderDetailService sliderDetailService,
                                      AppDbContext context,
                                      IWebHostEnvironment env)
        {
            _sliderDetailService = sliderDetailService;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index() => View(await _sliderDetailService.GetAllDatasAsync());

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            SliderDetail data = await _sliderDetailService.GetByIdAsync(id);

            if (data is null) return NotFound();

            await _sliderDetailService.DeleteAsync(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.SignImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("SignImage", "This file must be in image format");
                return View();
            }

            if (request.SignImage.CheckFileSize(200))
            {
                ModelState.AddModelError("SignImage", "Image size cannot be more than 200 KB");
                return View();
            }

            await _sliderDetailService.CreateAsync(request.Title, request.Description, request.SignImage);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            SliderDetail data = await _sliderDetailService.GetByIdAsync((int)id);

            if (data is null) return NotFound();

            return View(new SliderInfoEditVM { Title = data.Title, Description = data.Description, SignImage = data.SignImage });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM request)
        {
            if (id is null) return BadRequest();

            SliderDetail sliderDetail = await _sliderDetailService.GetByIdAsync((id));

            if (sliderDetail is null) return NotFound();

            if (request.NewSignImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewSignImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "This file must be in image format");
                request.SignImage = sliderDetail.SignImage;
                return View(request);
            }

            if (request.NewSignImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size cannot be more than 200 KB");
                request.SignImage = sliderDetail.SignImage;
                return View(request);
            }

            await _sliderDetailService.EditAsync(sliderDetail, request.NewSignImage, request.Title, request.Description);

            return RedirectToAction(nameof(Index));
        }
    }
}
