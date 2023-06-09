using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<Product> GetByIdWithIncludesAsync(int? id);
        ProductDetailVM GetMappedData(Product product);
    }
}
