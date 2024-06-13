using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class OrderDetailsContext : IDb<OrderDetail, int>
    {

        private readonly StorageDbContext _storageDbContext;

        public OrderDetailsContext(StorageDbContext storageDbContext)
        {
            _storageDbContext = storageDbContext;
        }

        public async Task CreateAsync(OrderDetail entity)
        {
            try
            {
                Shoe shoeFromDB = await _storageDbContext.Shoes.FindAsync(entity.Shoe.Id);

                if (shoeFromDB is not null)
                {
                    entity.Shoe = shoeFromDB;
                }

                Order orderFromDB = await _storageDbContext.Orders.FindAsync(entity.Order.Id);

                if (orderFromDB is not null)
                {
                    entity.Order = orderFromDB;
                }

                _storageDbContext.OrderDetails.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while creating an order detail entity.", ex);
            }
        }

        public async Task<OrderDetail> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<OrderDetail> query = _storageDbContext.OrderDetails;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Shoe).Include(t => t.Order);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(t => t.Id == key);

            }
            catch (Exception ex) 
            { 

                throw new Exception("An error occurred while reading the order detail entity.", ex); 
            }
        }

        public async Task<List<OrderDetail>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<OrderDetail> query = _storageDbContext.OrderDetails;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Shoe).Include(t => t.Order);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while reading all order detail entities.", ex);
            }
        }

        public async Task UpdateAsync(OrderDetail entity, bool useNavigationalProperties = false)
        {
            try
            {
                OrderDetail orderDetailFromDB = await ReadAsync(entity.Id, useNavigationalProperties, false);

                if (orderDetailFromDB is null)
                {
                    throw new KeyNotFoundException($"Order detail with id {entity.Id} not found.");
                }

                _storageDbContext.Entry(orderDetailFromDB).CurrentValues.SetValues(entity);

                if (useNavigationalProperties)
                {
                    Shoe shoeFromDb = _storageDbContext.Shoes.Find(entity.Shoe.Id);

                    if (shoeFromDb is not null)
                    {
                        orderDetailFromDB.Shoe = shoeFromDb;
                    }
                    else
                    {
                        orderDetailFromDB.Shoe = entity.Shoe;
                    }

                    Order orderFromDb = _storageDbContext.Orders.Find(entity.Order.Id);

                    if (orderFromDb is not null)
                    {
                        orderDetailFromDB.Order = orderFromDb;
                    }
                    else
                    {
                        orderDetailFromDB.Order = entity.Order;
                    }
                }

                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the order detail entity.", ex);
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                OrderDetail orderDetailFromDb = await ReadAsync(key, false, false);

                if (orderDetailFromDb is null)
                {
                    throw new ArgumentException("Order detail with id " + key + " does not exist in the database!");
                }

                _storageDbContext.OrderDetails.Remove(orderDetailFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the order detail entity.", ex);
            }
        }
    }
}
