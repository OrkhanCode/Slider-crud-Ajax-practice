using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Text.Json;

namespace Fiorello.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public CartController(AppDbContext context, 
                              IHttpContextAccessor accessor, 
                              IProductService productService,
                              IBasketService basketService)
        {
            _context = context;
            _accessor = accessor;
            _productService = productService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketDetailVM> basketList = new();

            if (_accessor.HttpContext.Session.GetString("basket") != null)
            {
                List<BasketVM> basketDatas = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));

                foreach (var item in basketDatas)
                {
                    var product = await _context.Products.Include("Images").FirstOrDefaultAsync(m => m.Id == item.Id);

                    if(product != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Image = product.Images.Where(m => !m.SoftDelete).FirstOrDefault().Image,
                            Count = item.Count,
                            Price = product.Price,
                            TotalPrice = item.Count * product.Price
                        };

                        basketList.Add(basketDetail);
                    }
                }
            }

            return View(basketList);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);

            if (product is null) return NotFound();

            List<BasketVM> basket = _basketService.GetAll();

            _basketService.AddProduct(basket, product);

            int basketCount = _basketService.GetCount();

            return Ok(basketCount);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProductFromBasket(int? id)
        {
            return Ok(await _basketService.DeleteProduct(id));
        }
    }
}
