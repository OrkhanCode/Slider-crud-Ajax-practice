using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Responses;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using System.Text.Json;

namespace Fiorello.Services
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public BasketService(AppDbContext context, 
                             IHttpContextAccessor accessor,
                             IProductService productService)
        {
            _context = context;
            _accessor = accessor;
            _productService = productService;
        }

        public void AddProduct(List<BasketVM> basket, Product product)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basket));
        }

        public async Task<BasketDeleteResponse> DeleteProduct(int? id)
        {
            var products = JsonSerializer.Deserialize<List<BasketDetailVM>>(_accessor.HttpContext.Session.GetString("basket"));

            var deleteProduct = products.FirstOrDefault(m => m.Id == id);

            int deleteIndex = products.IndexOf(deleteProduct);

            products.RemoveAt(deleteIndex);

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(products));

            decimal total = 0;

            foreach (var item in products)
            {
                Product dbProduct = await _productService.GetByIdAsync(item.Id);
                total += (dbProduct.Price * item.Count);
            }

            int count = products.Sum(m => m.Count);

            return new BasketDeleteResponse { Count = count, Total = total };
        }

        public List<BasketVM> GetAll()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Session.GetString("basket") != null)
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        public int GetCount()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Session.GetString("basket") != null)
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m => m.Count);
        }
    }
}
