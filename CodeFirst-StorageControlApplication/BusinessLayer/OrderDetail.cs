using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OrderDetail
    {
        [Key] 
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The quantity cannot be negative number.")]
        public int Quantity { get; set; }

        [Required]
        public Shoe Shoe { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeId { get; set; }

        private OrderDetail()
        {

        }

        // f(x) = x + 1

        public OrderDetail(int quantity,  Shoe shoe)
        {
            Quantity = quantity;
            Shoe = shoe;
            ShoeId = shoe.Id;
        }
    }
}
