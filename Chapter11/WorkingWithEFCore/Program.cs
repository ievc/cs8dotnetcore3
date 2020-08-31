using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



namespace WorkingWithEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //QueryingCategories();
            //QueryingWithLike();
            //QueryingProducts();

            /*if (AddProduct(6, "Bob's Burgers", 500M))
            {
                WriteLine("Add product successful.");
            }*/
            /*if (IncreaseProductPrice("Bob", 20M))
            {
                WriteLine("Update product price successfull");
            }*/
            int deletedProducts = DeleteProducts("Bob");
            WriteLine($"{deletedProducts} product() were deleted.");

            ListProducts();
            
        }

        static void ListProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}", "ID", "Product Name", "Cost", "Stock", "Disc.");
                foreach (var item in db.Products.OrderByDescending(p => p.Cost))
                {
                    WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
                    item.ProductID, item.ProductName, item.Cost,
                    item.Stock, item.Discontinued);
                }
            }
        }
        static bool AddProduct(int categoryID, string productName, decimal? price)
        {
            using (var db = new Northwind())
            {
                var newProduct = new Product
                {
                    CategoryID = categoryID,
                    ProductName = productName,
                    Cost = price
                };

                // mark product as added in change tracking
                db.Products.Add(newProduct);

                // save trached change to database
                var affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        static bool IncreaseProductPrice(string name, decimal amount)
        {
            using (var db = new Northwind())
            {
                // get first product whose name starts with name
                Product updateProduct = db.Products.First(p => p.ProductName.StartsWith(name));
                updateProduct.Cost += amount;
                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        static int DeleteProducts(string name)
        {
            using (var db = new Northwind())
            {
                using (IDbContextTransaction t = db.Database.BeginTransaction())
                {
                    WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);
                    var products = db.Products.Where(p => p.ProductName.StartsWith(name));
                    db.Products.RemoveRange(products);
                    int affected = db.SaveChanges();
                    t.Commit();
                    return affected;
                }
                
            }
        }

        static void QueryingCategories()
        {
            using(var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerPrivider());

                WriteLine("Categories and how many products they have:");

                // a query to get all categories and their related products
                IQueryable<Category> cats;
                    // = db.Categories;
                    //.Include(c =>c.Products);
                db.ChangeTracker.LazyLoadingEnabled = false;
                Write("Enable eager loading? (Y/N): ");
                bool eagerLoading = ReadKey().Key == ConsoleKey.Y;
                
                if (eagerLoading)
                {
                    cats = db.Categories.Include(c =>c.Products);
                }
                else
                {
                    cats = db.Categories;
                }

                Write("Enable explicit loading? (Y/N): ");
                bool explicitLoading = ReadKey().Key == ConsoleKey.Y;
                WriteLine();

                foreach (var c in cats)
                {
                    if (explicitLoading)
                    {
                        Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                        ConsoleKey key = ReadKey().Key;
                        WriteLine();
                        if (key == ConsoleKey.Y)
                        {
                            var products = db.Entry(c).Collection(c2 => c2.Products);
                            if (!products.IsLoaded) products.Load();
                        }
                    }
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }

        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerPrivider());

                WriteLine("products that costs more than a price, highest at top.");
                string input;

                decimal price;
                do
                {
                    Write("Enter a product price: "); 
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));
                WriteLine($"Product price: {price}"); 

                IQueryable<Product> prods = db.Products
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);
               

                foreach (var item in prods)
                {
                    WriteLine($"{item.ProductID}: {item.ProductName} costs {item.Cost:$#.##0.00} and has {item.Stock} in stock. ");
                }


            }
        }

        static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerPrivider());

                Write("Please enter part of product name: ");
                var input = ReadLine();
                IQueryable<Product> prods = db.Products
                    .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
                
                foreach (var item in prods)
                {
                    WriteLine($"{item.ProductName} has {item.Stock} units in stock. Discontinued? {item.Discontinued}");
                }
            }
        }

    }
}
