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

        public List<OrderDetail> OrderDetails { get; set; }

        private Order() 
        {
            OrderDetails = new List<OrderDetail>();
        }

        public Order(DateTime orderDate, Client client) : this()
        {
            OrderDate = orderDate;
            Client = client;
            ClientId = client.Id;
        }

        public Order(DateTime orderDate, Client client, List<OrderDetail> orderDetails)
        {
            OrderDate = orderDate;
            Client = client;
            ClientId = client.Id;
            OrderDetails = orderDetails;
        }

    }
}
