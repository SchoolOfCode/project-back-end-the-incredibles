using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    void Delete(long ProductId);
    Task<T> GetbyBusiness(long Id);

    Task<T> GetbyProduct(long ProductId);
    Task<T> UpdatebyBusiness(T t);

    Task<T> UpdatebyProduct(T t);
    Task<T> InsertbyBusiness(T t);
    Task<T> InsertbyProduct(T t);
    Task<IEnumerable<T>> Search(string s);
}