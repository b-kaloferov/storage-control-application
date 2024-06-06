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
        Task DiscardShoesAsync(int id, int quantity);
    }
}
