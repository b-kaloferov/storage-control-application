using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = BusinessLayer.Model;

namespace DataLayer
{
    public class ShoesContext : IDb<Shoe, int>
    {

        private readonly StorageDbContext _storageDbContext;

        public ShoesContext(StorageDbContext storageDbContext)
        { 
            _storageDbContext = storageDbContext;
        }

        public async Task CreateAsync(Shoe entity)
        {
            try
            {
                Model modelFromDB = _storageDbContext.Models.Find(entity.Model.Id);

                if (modelFromDB is not null)
                {
                    entity.Model = modelFromDB;
                }

                _storageDbContext.Shoes.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shoe> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shoe> query = _storageDbContext.Shoes;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Model);
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
        public async Task<List<Shoe>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shoe> query = _storageDbContext.Shoes;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Model);
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

        public async Task UpdateAsync(Shoe entity, bool useNavigationalProperties = false)
        {
            try
            {
                Shoe shoeFromDB = await ReadAsync(entity.Id, useNavigationalProperties, false);

                _storageDbContext.Entry(shoeFromDB).CurrentValues.SetValues(entity);

                if (useNavigationalProperties)
                {
                    Model modelFromDb = _storageDbContext.Models.Find(entity.Model.Id);

                    if (modelFromDb is not null)
                    {
                        shoeFromDB.Model = modelFromDb;
                    }
                    else
                    {
                        shoeFromDB.Model = entity.Model;
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
                Shoe shoeFromDb = await ReadAsync(key);

                if (shoeFromDb is null)
                {
                    throw new ArgumentException("Shoe with id " + key + " does not exist in the database!");
                }

                _storageDbContext.Shoes.Remove(shoeFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
