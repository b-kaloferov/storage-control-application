using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ClientsContext : IDb<Client, int>
    {

        private readonly StorageDbContext _storageDbContext;

        public ClientsContext(StorageDbContext storageDbContext)
        {
            _storageDbContext = storageDbContext;
        }

        public async Task CreateAsync(Client entity)
        {
            try
            {
                List<Order> orders = new List<Order>();

                foreach (var item in entity.Orders)
                {
                    Order orderFromDb = _storageDbContext.Orders.Find(item.Id);

                    if (orderFromDb is not null)
                    {
                        orders.Add(orderFromDb);
                    }
                    else
                    {
                        orders.Add(item);
                    }
                }

                entity.Orders = orders;

                _storageDbContext.Clients.Add(entity);
                _storageDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while creating a client entity.", ex);
            }
        }

        public async Task<Client> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Client> query = _storageDbContext.Clients;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Orders);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(e => e.Id == key);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while reading the client entity.", ex);
            }
        }
        
        public async Task<List<Client>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Client> query = _storageDbContext.Clients;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Orders);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while reading all client entities.", ex);
            }
        }

        public async Task UpdateAsync(Client entity, bool useNavigationalProperties = false)
        {
            try
            {
                Client clientFromDb = await ReadAsync(entity.Id, false, false);

                if (clientFromDb is null)
                {
                    throw new KeyNotFoundException($"Client with id {entity.Id} not found.");
                }
                
                _storageDbContext.Entry(clientFromDb).CurrentValues.SetValues(entity);

                List<Order> orders = new List<Order>();

                foreach (var item in entity.Orders)
                {
                    Order orderFromDb = _storageDbContext.Orders.Find(item.Id);

                    if (orderFromDb is not null)
                    {
                        orders.Add(orderFromDb);
                    }
                    else
                    {
                        orders.Add(item);
                    }
                }

                clientFromDb.Orders = orders;
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while updating the client entity.", ex);
            }
        }
        
        public async Task DeleteAsync(int key)
        {
            try
            {
                Client clientFromDb = await ReadAsync(key, false, false);

                if (clientFromDb is null)
                {
                    throw new ArgumentException("Client with id = " + key + "does not exist!");
                }

                _storageDbContext.Clients.Remove(clientFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while deleting the client entity.", ex);
            }
        }
    }
}
