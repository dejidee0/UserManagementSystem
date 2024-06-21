using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileData.Domain;

namespace UserProfileData.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int pageIndex, int pageSize, string category);
        Task<IEnumerable<Product>> GetAllProducts(); 
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
