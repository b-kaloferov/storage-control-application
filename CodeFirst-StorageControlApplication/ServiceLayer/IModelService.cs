using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IModelService
    {
        Task CreateModelAsync(Model model);
        Task<Model> GetModelByIdAsync(int id, bool useNavigationalProperties = false);
        Task<List<Model>> GetAllModelsAsync(bool useNavigationalProperties = false);
        Task UpdateModelAsync(Model model, bool useNavigationalProperties = false);
        Task DeleteModelAsync(int id);
    }
}
