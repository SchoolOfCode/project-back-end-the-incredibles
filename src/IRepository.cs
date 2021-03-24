using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    void Delete(long ProductId);
    Task<T> Get(long id);
    Task<T> Update(T t);
    Task<T> Insert(T t);
    Task<IEnumerable<T>> Search(string s);
}