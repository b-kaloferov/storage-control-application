using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IShoeService
    {
        Task CreateShoeAsync(Shoe shoe);
        Task<Shoe> GetShoeByIdAsync(int id, bool useNavigationalProperties = false);
        Task<List<Shoe>> GetShoesByModelIdAsync(int modelId);
        Task UpdateShoeAsync(Shoe shoe, bool useNavigationalProperties = false);
        Task DiscardShoesAsync(int id, int quantity);
    }
}
