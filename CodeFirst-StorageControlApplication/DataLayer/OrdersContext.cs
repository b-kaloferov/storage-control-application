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
    public class OrdersContext : IDb<Order, int>
    {
        private readonly StorageDbContext _storageDbContext;

        public OrdersContext(StorageDbContext storageDbContext)
        {
            _storageDbContext = storageDbContext;
        }

        public async Task CreateAsync(Order entity)
        {
            try
            {
                Client clientFromDb = _storageDbContext.Clients.Find(entity.Client.Id);

                if (clientFromDb is not null)
                {
                    entity.Client = clientFromDb;
                }

                OrderDetail orderDetailFromDb = _storageDbContext.OrderDetails.Find(entity.OrderDetail.Id);

                if (orderDetailFromDb is not null)
                {
                    entity.OrderDetail = orderDetailFromDb;
                }
                _storageDbContext.Orders.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating an order entity.", ex);
            }
            
        }

        public async Task<Order> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Order> query = _storageDbContext.Orders;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Client).Include(p => p.OrderDetail);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(t => t.Id == key);

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while reading the order entity.", ex);
            }
        }

        public async Task<List<Order>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Order> query = _storageDbContext.Orders;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Client).Include(p => p.OrderDetail);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while reading all order entities.", ex);
            }
        }

        public async Task UpdateAsync(Order entity, bool useNavigationalProperties = false)
        {
            try
            {
                Order orderFromDB = await ReadAsync(entity.Id, useNavigationalProperties, false);

                if (orderFromDB is null)
                {
                    throw new KeyNotFoundException($"Order with id {entity.Id} not found.");
                }

                _storageDbContext.Entry(orderFromDB).CurrentValues.SetValues(entity);

                if (useNavigationalProperties)
                {
                    Client clientFromDb = _storageDbContext.Clients.Find(entity.Client.Id);

                    if (clientFromDb is not null)
                    {
                        orderFromDB.Client = clientFromDb;
                    }
                    else
                    {
                        orderFromDB.Client = entity.Client;
                    }

                    OrderDetail orderDetailFromDB = _storageDbContext.OrderDetails.Find(entity.OrderDetail.Id);

                    if (orderDetailFromDB is not null)
                    {
                        orderFromDB.OrderDetail = orderDetailFromDB;
                    }
                    else
                    {
                        orderFromDB.OrderDetail = entity.OrderDetail;
                    }
                }

                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the order entity.", ex);
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Order orderFromDb = await ReadAsync(key, false, false);

                if (orderFromDb is null)
                {
                    throw new ArgumentException("Order with id " + key + " does not exist in the database!");
                }

                //за да се избегнат грешки, свързани с entity tracking, трябва да се уверим, че изтритата инстанция не се прикачва по повече от един начин в контекста
                //_storageDbContext.Entry(orderFromDb).State = EntityState.Detached;

                _storageDbContext.Orders.Remove(orderFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the order entity.", ex);
            }
        }
    }
}
