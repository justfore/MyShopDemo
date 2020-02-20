using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCat)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCat.Id);
            if (productCategoryToUpdate != null)
                productCategoryToUpdate = productCat;
            else
            {
                throw new Exception("product category not found");
            }
        }

        public void Delete(string productCatId)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == productCatId);
            if (productCategoryToDelete != null)
                productCategories.Remove(productCategoryToDelete);
            else
            {
                throw new Exception("product category not found");
            }
        }

        public ProductCategory Find(string productCategoryId)
        {
            ProductCategory productCategoryFound = productCategories.Find(p => p.Id == productCategoryId);
            if (productCategoryFound != null)
                return productCategoryFound;
            else
            {
                throw new Exception("product category not found");
            }
        }


        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

    }
}

