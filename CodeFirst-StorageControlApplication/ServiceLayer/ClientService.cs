using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ClientService : IClientService
    {

        private readonly ClientsContext _clientsContext;

        public ClientService(ClientsContext clientsContext)
        {
            _clientsContext = clientsContext;
        }

        public async Task CreateClientAsync(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("There is not implemented Client object.");
            }

            await _clientsContext.CreateAsync(client);
        }

        public async Task<Client> GetClientByIdAsync(int id, bool useNavigationalProperties = false)
        {
            return await _clientsContext.ReadAsync(id, useNavigationalProperties);
        }

        public async Task<List<Client>> GetAllClientsAsync(bool useNavigationalProperties = false)
        {
            return await _clientsContext.ReadAllAsync(useNavigationalProperties);
        }

        public async Task UpdateClientAsync(Client client, bool useNavigationalProperties = false)
        {
            if (client == null)
            {
                throw new ArgumentNullException("There is not implemented Client object.");
            }

            await _clientsContext.UpdateAsync(client, useNavigationalProperties);
        }
        
        public async Task DeleteClientAsync(int id)
        {
            await _clientsContext.DeleteAsync(id);
        }
    }
}
