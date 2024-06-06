//using BusinessLayer;
//using DataLayer;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NUnitTestingLayer
//{
//    public class OrdersContextTests
//    {
//        private StorageDbContext _dbContext;
//        private OrdersContext _ordersContext;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<StorageDbContext>()
//                .UseInMemoryDatabase(databaseName: "OrdersTestDb")
//                .Options;
//            _dbContext = new StorageDbContext(options);
//            _ordersContext = new OrdersContext(_dbContext);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _dbContext.Database.EnsureDeleted();
//            _dbContext.Dispose();
//        }

//        [Test]
//        public async Task CreateAsync_ShouldAddOrder()
//        {
//            // Arrange
//            var client = new Client("TestClient", "TestAddress", "testemail@test.com", "+123-654-321-098");
//            _dbContext.Clients.Add(client);
//            var orderDetail = new OrderDetail(1, new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description")));
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            var order = new Order(DateTime.Now, client, orderDetail);

//            // Act
//            await _ordersContext.CreateAsync(order);

//            // Assert
//            var orderFromDb = await _dbContext.Orders.Include(s => s.Client).Include(o => o.OrderDetail).FirstOrDefaultAsync(s => s.Id == order.Id);
//            Assert.IsNotNull(orderFromDb, "Order was not added to the database.");
//            Assert.AreEqual(order.Client, orderFromDb.Client);
//            Assert.AreEqual(order.OrderDetail, orderFromDb.OrderDetail);

//        }

//        [Test]
//        public async Task ReadAsync_ShouldReturnOrderFromDatabase()
//        {
//            // Arrange
//            var client = new Client("TestClient", "TestAddress", "testemail@test.com", "+123-654-321-098");
//            _dbContext.Clients.Add(client);
//            var orderDetail = new OrderDetail(1, new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description")));
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            var order = new Order(DateTime.Now, client, orderDetail);
//            _dbContext.Orders.Add(order);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var orderFromDb = await _ordersContext.ReadAsync(order.Id, useNavigationalProperties: true);

//            // Assert
//            Assert.IsNotNull(orderFromDb, "Order was not found in the database.");
//            Assert.AreEqual(order.Client.Id, orderFromDb.Client.Id, "Client Ids do not match.");
//            Assert.AreEqual(order.OrderDetail.Id, orderFromDb.OrderDetail.Id, "OrderDetail Ids do not match.");
//        }

//        [Test]
//        public async Task ReadAllAsync_ShouldReturnAllOrdersFromDatabase()
//        {
//            // Arrange
//            var client = new Client("TestClient", "TestAddress", "testemail@test.com", "+123-654-321-098");
//            _dbContext.Clients.Add(client);
//            var orderDetail = new OrderDetail(1, new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description")));
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            var orders = new List<Order>
//            {
//                new Order(DateTime.Now, client, orderDetail),
//                new Order(new DateTime(2002, 11, 1), client, orderDetail)
//            };

//            _dbContext.Orders.AddRange(orders);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var ordersFromDb = await _ordersContext.ReadAllAsync();

//            // Assert
//            Assert.AreEqual(orders.Count, ordersFromDb.Count, "The number of shoes returned does not match the number of shoes added.");
//        }

//        [Test]
//        public async Task UpdateAsync_ShouldUpdateOrderInDatabase()
//        {
//            // Arrange
//            var client = new Client("TestClient", "TestAddress", "testemail@test.com", "+123-654-321-098");
//            _dbContext.Clients.Add(client);
//            var orderDetail = new OrderDetail(1, new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description")));
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            var order = new Order(DateTime.Now, client, orderDetail);
//            _dbContext.Orders.Add(order);
//            await _dbContext.SaveChangesAsync();

//            var updatedClient = new Client("UpdatedClient", "UpdatedAddress", "update@gmail.com", "+123-654-321-000");
//            _dbContext.Clients.Add(updatedClient);
//            await _dbContext.SaveChangesAsync();

//            order.OrderDate = new DateTime(2000, 10, 10);
//            order.Client = updatedClient;

//            // Act
//            await _ordersContext.UpdateAsync(order);

//            // Assert
//            var orderFromDb = await _dbContext.Orders.Include(c => c.Client).Include(o => o.OrderDetail).FirstOrDefaultAsync(o => o.Id == order.Id);
//            Assert.IsNotNull(orderFromDb, "Order was not found in the database.");
//            Assert.AreEqual(order.OrderDate, orderFromDb.OrderDate);
//            Assert.AreEqual(order.Client, orderFromDb.Client);

//        }

//        [Test]
//        public async Task DeleteAsync_ShouldRemoveOrderFromDatabase()
//        {
//            // Arrange
//            var client = new Client("TestClient", "TestAddress", "testemail@test.com", "+123-654-321-098");
//            _dbContext.Clients.Add(client);
//            var orderDetail = new OrderDetail(1, new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description")));
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            var order = new Order(DateTime.Now, client, orderDetail);
//            _dbContext.Orders.Add(order);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var orderToDelete = await _dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == order.Id);
//            Assert.IsNotNull(orderToDelete, "Order to delete was not found in the database.");

//            await _ordersContext.DeleteAsync(orderToDelete.Id);

//            // Assert
//            var orderFromDb = await _dbContext.Orders.FindAsync(order.Id);
//            Assert.IsNull(orderFromDb, "Order was not removed from the database.");
//        }
//    }
//}
