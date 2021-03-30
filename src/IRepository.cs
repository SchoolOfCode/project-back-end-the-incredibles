using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    void DeletebyBusiness(long ProductId);
    void DeletebyProduct(long ProductId);
    Task<Product> GetbyProduct(long ProductId);
    Task<T> UpdatebyBusiness(T t);
    Task<Business> UpdatebyProduct(T t);
    Task<T> InsertbyBusiness(T t);
    Task<Product> InsertbyProduct(Product product);
    Task<T> GetbyBusiness(string Auth);

    Task<IEnumerable<Product>> GetProducts(long id);

    Task<IEnumerable<T>> Search(string s);
}