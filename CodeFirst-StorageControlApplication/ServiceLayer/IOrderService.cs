using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IOrderService
    {
        Task MakePurchaseAsync(Order order);
        Task<Order> GetPurchaseByIdAsync(int id, bool useNavigationalProperties = false);
        Task<List<Order>> GetPurchaseHistoryAsync(int clientId, bool useNavigationalProperties = false);
        Task<List<Order>> GetAllPurchasesHistoryAsync(bool useNavigationalProperties = false);
        Task UpdateOrderAsync(Order order, bool useNavigationalProperties = false);
        Task DeleteOrderAsync(int id);
    }
}
