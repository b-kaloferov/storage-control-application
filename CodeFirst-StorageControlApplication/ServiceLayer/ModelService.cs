using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ModelService : IModelService
    {

        private readonly ModelsContext _modelsContext;

        public ModelService(ModelsContext modelsContext)
        {
            _modelsContext = modelsContext;
        }

        public async Task CreateModelAsync(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("There is not implemented Model object.");
            }

            await _modelsContext.CreateAsync(model);
        }

        public async Task<Model> GetModelByIdAsync(int id, bool useNavigationalProperties = false)
        {
            return await _modelsContext.ReadAsync(id, useNavigationalProperties);
        }
        
        public async Task<List<Model>> GetAllModelsAsync(bool useNavigationalProperties = false)
        {
            return await _modelsContext.ReadAllAsync(useNavigationalProperties);
        }

        public async Task UpdateModelAsync(Model model, bool useNavigationalProperties = false)
        {
            if (model == null)
            {
                throw new ArgumentNullException("There is not implemented Model object.");
            }

            await _modelsContext.UpdateAsync(model, useNavigationalProperties);
        }
        
        public async Task DeleteModelAsync(int id)
        {
            await _modelsContext.DeleteAsync(id);
        }

    }
}
