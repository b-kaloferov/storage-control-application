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
//    public class OrderDetailsContextTests
//    {
//        private StorageDbContext _dbContext;
//        private OrderDetailsContext _orderDetailsContext;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<StorageDbContext>()
//                .UseInMemoryDatabase(databaseName: "OrderDetailsTestDb")
//                .Options;

//            _dbContext = new StorageDbContext(options);
//            _orderDetailsContext = new OrderDetailsContext(_dbContext);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _dbContext.Database.EnsureDeleted();
//            _dbContext.Dispose();
//        }

//        [Test]
//        public async Task CreateAsync_ShouldAddOrderDetailToDatabase()
//        {
//            // Arrange
//            var shoe = new Shoe(42, 2, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description"));
//            _dbContext.Shoes.Add(shoe);
//            await _dbContext.SaveChangesAsync();

//            var orderDetail = new OrderDetail(1, shoe);
          

//            // Act
//            await _orderDetailsContext.CreateAsync(orderDetail);

//            // Assert
//            var orderDetailFromDb = await _dbContext.OrderDetails.Include(s => s.Shoe).FirstOrDefaultAsync(o => o.Id == orderDetail.Id);
//            Assert.IsNotNull(orderDetailFromDb, "Order detail was not added to the database.");
//            Assert.AreEqual(orderDetail.Quantity, orderDetailFromDb.Quantity);
//            Assert.AreEqual(orderDetail.Shoe, orderDetailFromDb.Shoe);
            
//        }

//        [Test]
//        public async Task ReadAsync_ShouldReturnOrderDetailFromDatabase()
//        {
//            // Arrange
//            var shoe = new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description"));
//            _dbContext.Shoes.Add(shoe);
//            await _dbContext.SaveChangesAsync();

//            var orderDetail = new OrderDetail(1, shoe);
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var orderDetailFromDb = await _orderDetailsContext.ReadAsync(orderDetail.Id, useNavigationalProperties: true);

//            // Assert
//            Assert.IsNotNull(orderDetailFromDb, "OrderDetail was not found in the database.");
//            Assert.AreEqual(orderDetail.Quantity, orderDetailFromDb.Quantity);
//            Assert.AreEqual(orderDetail.Shoe.Id, orderDetailFromDb.Shoe.Id);
//        }

//        [Test]
//        public async Task ReadAllAsync_ShouldReturnAllOrderDetailsFromDatabase()
//        {
//            // Arrange
//            var shoe1 = new Shoe(42, 10, new Model("TestBrand1", "Code123", "Sneakers", 99.99m, "Men", "Test description 1"));
//            var shoe2 = new Shoe(43, 15, new Model("TestBrand2", "Code456", "Boots", 149.99m, "Women", "Test description 2"));
//            _dbContext.Shoes.AddRange(shoe1, shoe2);
//            await _dbContext.SaveChangesAsync();

//            var orderDetail1 = new OrderDetail(1, shoe1);
//            var orderDetail2 = new OrderDetail(2, shoe2);
//            _dbContext.OrderDetails.AddRange(orderDetail1, orderDetail2);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var orderDetailsFromDb = await _orderDetailsContext.ReadAllAsync(useNavigationalProperties: true);

//            // Assert
//            Assert.AreEqual(2, orderDetailsFromDb.Count, "Not all OrderDetails were returned from the database.");
//        }

//        [Test]
//        public async Task UpdateAsync_ShouldUpdateOrderDetailInDatabase()
//        {
//            // Arrange
//            var shoe = new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description"));
//            _dbContext.Shoes.Add(shoe);
//            await _dbContext.SaveChangesAsync();

//            var orderDetail = new OrderDetail(1, shoe);
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();
//            orderDetail.Quantity = 5;

//            // Act
//            await _orderDetailsContext.UpdateAsync(orderDetail);

//            // Assert
//            var orderDetailFromDb = await _dbContext.OrderDetails.FirstOrDefaultAsync(od => od.Id == orderDetail.Id);
//            Assert.IsNotNull(orderDetailFromDb, "OrderDetail was not found in the database.");
//            Assert.AreEqual(5, orderDetailFromDb.Quantity, "OrderDetail was not updated in the database.");
//        }

//        [Test]
//        public async Task DeleteAsync_ShouldRemoveOrderDetailFromDatabase()
//        {
//            // Arrange
//            var shoe = new Shoe(42, 10, new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description"));
//            _dbContext.Shoes.Add(shoe);
//            await _dbContext.SaveChangesAsync();

//            var orderDetail = new OrderDetail(1, shoe);
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            // Act
//            var orderDetailToDelete = await _dbContext.OrderDetails.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderDetail.Id);
//            await _orderDetailsContext.DeleteAsync(orderDetailToDelete.Id);

//            // Assert
//            var orderDetailFromDb = await _dbContext.OrderDetails.FindAsync(orderDetail.Id);
//            Assert.IsNull(orderDetailFromDb, "Order detail was not removed from the database.");
//        }
//    }

    
//}
