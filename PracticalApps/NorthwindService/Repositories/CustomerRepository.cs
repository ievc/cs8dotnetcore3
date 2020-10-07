using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using Packt.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NorthwindService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        // use static thread-save dictionary field to cache customers
        private static ConcurrentDictionary<string, Customer> customerCache;

        // use an instance data context field because it should not be cached due to their internal caching
        private Northwind db;

        public CustomerRepository(Northwind db)
        {
            this.db = db;

            // preload customers from database as a normal Dictionary with CustomerID as the key
            // then convert to thread-safe ConcurrentDictionary
            if (customerCache == null)
            {
                customerCache = new ConcurrentDictionary<string, Customer>(db.Customers.ToDictionary(c=>c.CustomerID));
            }
        }



        public async Task<Customer> CreateAsync(Customer c)
        {
            // Normalize CustomerID into uppercase
            c.CustomerID = c.CustomerID.ToUpper();

            // add to database using EF Core
            EntityEntry<Customer> added = await db.Customers.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if(affected == 1)
            {
                // if a customer is new, add it to cache, else call UpdateCache method
                return customerCache.AddOrUpdate(c.CustomerID, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            // for performance, get from cache
            return Task.Run<IEnumerable<Customer>>(
                () => customerCache.Values
            );
        }

        public Task<Customer> RetrieveAsync(string id)
        {
            // for performance, get from cache
            return Task.Run<Customer>( () =>
            {
                id = id.ToUpper();
                Customer c;
                customerCache.TryGetValue(id, out c);
                return c;
            });
        }

        private Customer UpdateCache(string id, Customer c)
        {
            Customer old;
            if (customerCache.TryGetValue(id, out old))
            {
                if (customerCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }

        public async Task<Customer> UpdateAsync(string id, Customer c)
        {
            // Normalize customer id
            id = id.ToUpper();
            c.CustomerID = c.CustomerID.ToUpper();
            
            // update in database
            db.Customers.Update(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                UpdateCache(id, c);
            }

            return null;
        }
        
        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            Customer c = db.Customers.Find(id);
            db.Customers.Remove(c);
            int affected = await db.SaveChangesAsync();
            if(affected == 1)
            {
                // try to remove from cache
                customerCache.TryRemove(id, out c);
            }

            return null;
        }
    }
}

