﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
   
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }

        // GET: ProductManagement
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string productId)
        {
            Product product = context.Find(productId);
            if (product == null)
                return HttpNotFound();
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    productToEdit.Category = product.Category;
                    productToEdit.Name = product.Name;
                    productToEdit.Description = product.Description;
                    productToEdit.Price = product.Price;
                    productToEdit.Image = product.Image;

                    context.Commit();
                    return RedirectToAction("Index");

                }
            }
        }

        public ActionResult Delete(string productId)
        {
            Product product = context.Find(productId);
            if (product == null)
                return HttpNotFound();
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string productId)
        {
            Product product = context.Find(productId);
            if (product == null)
                return HttpNotFound();
            else
            {
                context.Delete(productId);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}