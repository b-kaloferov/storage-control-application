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

                List<OrderDetail> orderDetails = new List<OrderDetail>();

                foreach (var item in entity.OrderDetails)
                {
                    OrderDetail orderDetailFromDb = _storageDbContext.OrderDetails.Find(item.Id);

                    if (orderDetailFromDb is not null)
                    {
                        orderDetails.Add(orderDetailFromDb);
                    }
                    else
                    {
                        orderDetails.Add(item);
                    }
                }

                entity.OrderDetails = orderDetails;

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
                    query = query.Include(t => t.Client).Include(p => p.OrderDetails);
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
                    query = query.Include(t => t.Client).Include(p => p.OrderDetails);
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
                Order orderFromDb = await ReadAsync(entity.Id, useNavigationalProperties, false);

                if (orderFromDb is null)
                {
                    throw new KeyNotFoundException($"Order with id {entity.Id} not found.");
                }

                _storageDbContext.Entry(orderFromDb).CurrentValues.SetValues(entity);

                if (useNavigationalProperties)
                {
                    Client clientFromDb = _storageDbContext.Clients.Find(entity.Client.Id);

                    if (clientFromDb is not null)
                    {
                        orderFromDb.Client = clientFromDb;
                    }
                    else
                    {
                        orderFromDb.Client = entity.Client;
                    }

                    List<OrderDetail> orderDetails = new List<OrderDetail>();

                    foreach (var item in entity.OrderDetails)
                    {
                        OrderDetail orderDetailFromDb = _storageDbContext.OrderDetails.Find(item.Id);

                        if (orderDetailFromDb is not null)
                        {
                            orderDetails.Add(orderDetailFromDb);
                        }
                        else
                        {
                            orderDetails.Add(item);
                        }
                    }

                    orderFromDb.OrderDetails = orderDetails;
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
