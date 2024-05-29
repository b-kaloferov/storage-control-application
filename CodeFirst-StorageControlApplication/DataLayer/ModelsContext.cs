using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ModelsContext : IDb<Model, int>
    {
        private readonly StorageDbContext _storageDbContext;

        public ModelsContext(StorageDbContext storageDbContext)
        {
            _storageDbContext = storageDbContext;
        }
        public async Task CreateAsync(Model entity)
        {
            try
            {
                _storageDbContext.Models.Add(entity);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while creating a model entity.", ex);
            }
        }

        public async Task<Model> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Model> query = _storageDbContext.Models;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Shoes);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(e => e.Id == key);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while reading the model entity.", ex);
            }
        }

        public async Task<List<Model>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Model> query = _storageDbContext.Models;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Shoes);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while reading all model entities.", ex);
            }
        }

        public async Task UpdateAsync(Model entity, bool useNavigationalProperties = false)
        {
            try
            {
                Model modelFromDb = await ReadAsync(entity.Id, false, false);

                if (modelFromDb is null)
                {
                    throw new KeyNotFoundException($"Model with id {entity.Id} not found.");
                }

                _storageDbContext.Entry(modelFromDb).CurrentValues.SetValues(entity);

                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the model entity.", ex);
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Model modelFromDb = await ReadAsync(key, false, false);
                
                if (modelFromDb is null)
                {
                    throw new ArgumentException("Model with id = " + key + "does not exist!");
                }

                _storageDbContext.Models.Remove(modelFromDb);
                await _storageDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the model entity.", ex);
            }
        }
    }
}
