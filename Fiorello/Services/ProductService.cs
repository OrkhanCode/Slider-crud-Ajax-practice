using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() => await _context.Products.Include(m => m.Category).Include(m => m.Images).Include(m => m.Discount).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Product> GetByIdAsync(int? id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetByIdWithIncludesAsync(int? id)
        {
            return await _context.Products.Where(m => m.Id == id)
                                          .Include(m => m.Category)
                                          .Include(m => m.Images)
                                          .Include(m => m.Discount)
                                          .FirstOrDefaultAsync();
        }

        public ProductDetailVM GetMappedData(Product product)
        {
            return new ProductDetailVM
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString("0.####"),
                DiscountName = product.Discount.Name,
                CategoryName = product.Category.Name,
                CreateDate = product.CreatedDate.ToString("dd/MM/yyyy"),
                Images = product.Images.Select(m => m.Image)
            };
        }

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ProductVM> model = new();

            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Images.Where(m => m.IsMain).FirstOrDefault().Image,
                    CategoryName = product.Category.Name,
                    DiscountName = product.Discount.Name,
                });
            }

            return model;
        }
    }
}
