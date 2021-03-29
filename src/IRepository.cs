using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    void DeletebyBusiness(long ProductId);
    void DeletebyProduct(long ProductId);
    Task<T> GetbyProduct(long ProductId);
    Task<T> UpdatebyBusiness(T t);
    Task<T> UpdatebyProduct(T t);
    Task<T> InsertbyBusiness(T t);
    Task<T> InsertbyProduct(T t);
    Task<T> GetbyBusiness(string Auth);

    Task<IEnumerable<Product>> GetProducts(long id);

    Task<IEnumerable<T>> Search(string s);
}