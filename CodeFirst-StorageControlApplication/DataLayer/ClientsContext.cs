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
                _storageDbContext.Clients.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Client entity, bool useNavigationalProperties = false)
        {
            try
            {
                Client clientFromDb = await ReadAsync(entity.Id, false, false);

                if (clientFromDb is null)
                {
                    throw new ArgumentException("Client with id = " + entity.Id + "does not exist!");
                }

                _storageDbContext.Entry(clientFromDb).CurrentValues.SetValues(entity);

                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
