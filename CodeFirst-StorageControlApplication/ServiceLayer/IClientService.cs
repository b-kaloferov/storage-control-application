using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IClientService
    {
        Task CreateClientAsync(Client client);
        Task<Client> GetClientByIdAsync(int id, bool useNavigationalProperties = false);
        Task<List<Client>> GetAllClientsAsync(bool useNavigationalProperties = false);
        Task UpdateClientAsync(Client client, bool useNavigationalProperties = false);
        Task DeleteClientAsync(int id);
    }
}
