using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {

        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T p)
        {
            items.Add(p);
        }

        public void Update(T t)
        {
            T TToUpdate = items.Find(p => p.Id == t.Id);
            if (TToUpdate != null)
                TToUpdate = t;
            else
            {
                throw new Exception("T not found");
            }
        }

        public void Delete(string TId)
        {
            T ToDelete = items.Find(p => p.Id == TId);
            if (ToDelete != null)
                items.Remove(ToDelete);
            else
            {
                throw new Exception("T not found");
            }
        }



        public T Find(string productId)
        {
            T product = items.Find(p => p.Id == productId);
            if (product != null)
                return product;
            else
            {
                throw new Exception("product not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

    }
}
