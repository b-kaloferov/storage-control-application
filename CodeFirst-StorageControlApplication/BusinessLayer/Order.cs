using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public Client Client { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        public OrderDetail OrderDetail { get; set; }

        [ForeignKey("OrderDetail")]
        public int OrderDetailId { get; set; }

        private Order() 
        {

        }

        public Order(DateTime orderDate, Client client, OrderDetail orderDetail)
        {
            OrderDate = orderDate;
            Client = client;
            ClientId = client.Id;
            OrderDetail = orderDetail;
            OrderDetailId = orderDetail.Id;
        }
    }
}
