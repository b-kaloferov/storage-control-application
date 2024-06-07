using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderService : IOrderService
    {

        private readonly OrdersContext _ordersContext;
        private readonly OrderDetailsContext _orderDetailsContext;
        private readonly ClientsContext _clientsContext;
        private readonly ShoesContext _shoesContext;

        public OrderService(OrdersContext ordersContext, OrderDetailsContext orderDetailsContext, ClientsContext clientsContext, ShoesContext shoesContext)
        {
            _ordersContext = ordersContext;
            _orderDetailsContext = orderDetailsContext;
            _clientsContext = clientsContext;
            _shoesContext = shoesContext;
        }

        public async Task MakePurchaseAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("There is no Order object created.");
            }

            var client = await _clientsContext.ReadAsync(order.ClientId);
            if (client == null)
            {
                throw new ArgumentException("Client does not exist.");
            }

            foreach (var detail in order.OrderDetails)
            {
                var shoe = await _shoesContext.ReadAsync(detail.ShoeId);
                if (shoe == null)
                {
                    throw new ArgumentException($"Shoe with ID {detail.ShoeId} does not exist.");
                }

                detail.Shoe = shoe;
                await _orderDetailsContext.CreateAsync(detail);
            }

            order.Client = client;
            await _ordersContext.CreateAsync(order);
        }
        
        public async Task<Order> GetPurchaseByIdAsync(int id, bool useNavigationalProperties = false)
        {
            return await _ordersContext.ReadAsync(id, useNavigationalProperties);
        }

        public async Task<List<Order>> GetPurchaseHistoryAsync(int clientId, bool useNavigationalProperties = false)
        {
            var orders = await _ordersContext.ReadAllAsync(useNavigationalProperties);
            return orders.Where(o => o.ClientId == clientId).ToList();
        }
        
        public async Task<List<Order>> GetAllPurchasesHistoryAsync(bool useNavigationalProperties = false)
        {
            return await _ordersContext.ReadAllAsync(useNavigationalProperties);
        }

        public async Task UpdateOrderAsync(Order order, bool useNavigationalProperties = false)
        {
            if (order == null)
            {
                throw new ArgumentNullException("There is no Order object created.");
            }

            await _ordersContext.UpdateAsync(order, useNavigationalProperties);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _ordersContext.DeleteAsync(id);
        }
    }
}
