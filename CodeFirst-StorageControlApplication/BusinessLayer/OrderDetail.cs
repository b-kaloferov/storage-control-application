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

        [Required]
        public Order Order { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        private OrderDetail()
        {

        }

        public OrderDetail(int quantity,  Shoe shoe, Order order)
        {
            Quantity = quantity;
            Shoe = shoe;
            ShoeId = shoe.Id;
            Order = order;
            OrderId = order.Id;
        }
    }
}
