using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestingLayer
{
    public class ClientsContextTest
    {
        private StorageDbContext _dbContext;
        private ClientsContext _clientsContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StorageDbContext>()
                .UseInMemoryDatabase(databaseName: "ClientsTestDb")
                .Options;
            _dbContext = new StorageDbContext(options);
            _clientsContext = new ClientsContext(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddClient()
        {
            //Arrange
            var client = new Client("John Doe", "123 Main St", "john.doe@example.com", "+123-456-789-012");

            //Act
            await _clientsContext.CreateAsync(client);
            var clientFromDb = await _clientsContext.ReadAsync(client.Id);

            //Asserrt
            Assert.IsNotNull(clientFromDb);
            Assert.AreEqual(client.Name, clientFromDb.Name);
        }

        [Test]
        public async Task ReadAsync_ShouldReturnClient()
        {
            //Arrange
            var client = new Client("Jane Doe", "456 Elm St", "jane.doe@example.com", "+123-654-321-098");

            //Act
            await _clientsContext.CreateAsync(client);
            var clientFromDb = await _clientsContext.ReadAsync(client.Id);

            //Assert
            Assert.IsNotNull(clientFromDb);
            Assert.AreEqual(client.Name, clientFromDb.Name);
        }

        [Test]
        public async Task ReadAllAsync_ShouldReturnAllClients()
        {
            //Arrange
            var client1 = new Client("Alice", "789 Pine St", "alice@example.com", "+123-789-456-123");
            var client2 = new Client("Bob", "101 Oak St", "bob@example.com", "+123-987-654-321");

            //Act
            await _clientsContext.CreateAsync(client1);
            await _clientsContext.CreateAsync(client2);
            var clientsFromDb = await _clientsContext.ReadAllAsync();

            //Assert
            Assert.AreEqual(2, clientsFromDb.Count);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateClient()
        {
            //Arrange
            var client = new Client("Charlie", "202 Birch St", "charlie@example.com", "+123-321-654-987");

            //Act
            await _clientsContext.CreateAsync(client);
            client.Email = "charlie.updated@example.com";
            await _clientsContext.UpdateAsync(client);
            var clientFromDb = await _clientsContext.ReadAsync(client.Id);

            //Assert
            Assert.AreEqual("charlie.updated@example.com", clientFromDb.Email);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveClient()
        {
            //Arrange
            var client = new Client("Dave", "303 Maple St", "dave@example.com", "+123-654-789-321");

            //Act
            await _clientsContext.CreateAsync(client);
            await _clientsContext.DeleteAsync(client.Id);
            var clientFromDb = await _clientsContext.ReadAsync(client.Id);

            //Assert
            Assert.IsNull(clientFromDb);
        }

    }
}
