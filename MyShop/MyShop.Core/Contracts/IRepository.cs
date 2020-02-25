using MyShop.Core.Models;
using System.Linq;

namespace MyShop.Core.Models
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string TId);
        T Find(string productId);
        void Insert(T p);
        void Update(T t);
    }
}