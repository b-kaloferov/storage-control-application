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
                Shoe shoeFromDB = _storageDbContext.Shoes.Find(entity.Shoe.Id);

                if (shoeFromDB is not null)
                {
                    entity.Shoe = shoeFromDB;
                }

                _storageDbContext.OrderDetails.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OrderDetail> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<OrderDetail> query = _storageDbContext.OrderDetails;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Shoe);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(t => t.Id == key);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OrderDetail>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<OrderDetail> query = _storageDbContext.OrderDetails;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Shoe);
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
            };
        }

        public async Task UpdateAsync(OrderDetail entity, bool useNavigationalProperties = false)
        {
            try
            {
                OrderDetail orderDetailFromDB = await ReadAsync(entity.Id, useNavigationalProperties, false);

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
                }

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
                OrderDetail orderDetailFromDb = await ReadAsync(key);

                if (orderDetailFromDb is null)
                {
                    throw new ArgumentException("Order detail with id " + key + " does not exist in the database!");
                }

                _storageDbContext.OrderDetails.Remove(orderDetailFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
