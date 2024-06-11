using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ShoeService : IShoeService
    {
        private readonly ShoesContext _shoesContext;

        public ShoeService(ShoesContext shoesContext)
        {
            _shoesContext = shoesContext;
        }

        public async Task CreateShoeAsync(Shoe shoe)
        {
            if (shoe == null)
            {
                throw new ArgumentNullException("There is not implemented Shoe object.");
            }

            await _shoesContext.CreateAsync(shoe);
        }

        public async Task<Shoe> GetShoeByIdAsync(int id, bool useNavigationalProperties = false)
        {
            return await _shoesContext.ReadAsync(id, useNavigationalProperties);
        }

        public async Task UpdateShoeAsync(Shoe shoe, bool useNavigationalProperties = false)
        {
            if (shoe == null)
            {
                throw new ArgumentNullException("There is not implemented Shoe object.");
            }

            await _shoesContext.UpdateAsync(shoe, useNavigationalProperties);
        }

        public async Task DiscardShoesAsync(int id, int quantity)
        {
            var shoes = await _shoesContext.ReadAllAsync();
            var shoesToDiscard = shoes.Where(s => s.Id == id).Take(quantity).ToList();

            foreach (var shoe in shoesToDiscard)
            {
                await _shoesContext.DeleteAsync(shoe.Id);
            }
        }
    }
}
