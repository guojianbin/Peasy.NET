﻿using AutoMapper;
using Facile;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class ProductRepository : IProductDataProxy 
    {
        public ProductRepository()
        {
            Mapper.CreateMap<Product, Product>();
        }

        private List<Product> _products;

        private List<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new List<Product>() 
                    {
                        new Product() { ProductID = 1, Name = "Led Zeppeling I", Description = "Best album ever", Price=10, CategoryID=3 },
                        new Product() { ProductID = 2, Name = "Fender Strat", Description = "Blue guitar", Price=1000, CategoryID=1 },
                        new Product() { ProductID = 3, Name = "Laptop", Price=1000, CategoryID=2},
                        new Product() { ProductID = 4, Name = "Led Zeppeling II", Description = "Second best album ever", Price=11, CategoryID=3 },
                        new Product() { ProductID = 5, Name = "Gibson Les Paul", Description = "Red guitar", Price=3000, CategoryID=1 },
                        new Product() { ProductID = 6, Name = "Desktop", Price=500.5M, CategoryID=2},
                        new Product() { ProductID = 7, Name = "Led Zeppeling III", Description = "Third best album ever", Price=12, CategoryID=3 },
                        new Product() { ProductID = 8, Name = "PRS Solid Body", Description = "Orange guitar", Price=2000, CategoryID=1 },
                        new Product() { ProductID = 9, Name = "Led Zeppeling IV", Description = "Fourth best album ever", Price=11, CategoryID=3 },
                        new Product() { ProductID = 10, Name = "PRS Hollow Body", Description = "Green guitar", Price=2500, CategoryID=1 }
                    };
                }
                return _products;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            Debug.WriteLine("Executing EF Product.GetAll");
            // Simulate a SELECT against a database
            return Products.Select(Mapper.Map<Product, Product>).ToArray();
        }

        public Product GetByID(long id)
        {
            Debug.WriteLine("Executing EF Product.GetByID");
            return Products.First(c => c.ID == id);
        }

        public Product Insert(Product entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING product into database");
            var nextID = _products.Max(c => c.ID) + 1;
            entity.ID = nextID;
            Products.Add(entity);
            return entity;
        }

        public Product Update(Product entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("UPDATING product in database");
            var existing = Products.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING product in database");
            var product = Products.First(c => c.ID == id);
            Products.Remove(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<Product> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<Product> InsertAsync(Product entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            return Task.Run(() => Update(entity));
        }

        public Task DeleteAsync(long id)
        {
            return Task.Run(() => Delete(id));
        }

        public bool SupportsTransactions
        {
            get { return true; }
        }

        public bool IsLatencyProne
        {
            get { return false; }
        }
    }
}
