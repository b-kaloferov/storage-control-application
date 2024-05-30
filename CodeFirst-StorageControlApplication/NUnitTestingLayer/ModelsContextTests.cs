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
    public class ModelsContextTests
    {
        private StorageDbContext _dbContext;
        private ModelsContext _modelsContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StorageDbContext>()
                .UseInMemoryDatabase(databaseName: "ModelsTestDb")
                .Options;
            _dbContext = new StorageDbContext(options);
            _modelsContext = new ModelsContext(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddModel()
        {
            //Arrange
            var model = new Model("Nike", "A123", "Sneakers", 99.99m, "Men", "Test description");

            //Act
            await _modelsContext.CreateAsync(model);
            var modelFromDb = await _modelsContext.ReadAsync(model.Id);

            //Assert
            Assert.IsNotNull(modelFromDb);
            Assert.AreEqual(model.Brand, modelFromDb.Brand);
        }

        [Test]
        public async Task ReadAsync_ShouldReturnModel_WhenModelExists()
        {
            //Arrange
            var model = new Model("Adidas", "B456", "Running", 120.00m, "Women", "Test description");

            //Act
            await _modelsContext.CreateAsync(model);
            var modelFromDb = await _modelsContext.ReadAsync(model.Id);

            //Assert
            Assert.IsNotNull(modelFromDb);
            Assert.AreEqual(model.Brand, modelFromDb.Brand);
        }

        [Test]
        public async Task ReadAllAsync_ShouldReturnAllModels()
        {
            //Arrange
            var model1 = new Model("Puma", "C789", "Casual", 80.00m, "Unisex", "Test description");
            var model2 = new Model("Reebok", "D012", "Fitness", 70.00m, "Men", "Test description");

            //Act
            await _modelsContext.CreateAsync(model1);
            await _modelsContext.CreateAsync(model2); 
            var modelsFromDb = await _modelsContext.ReadAllAsync();

            //Assert
            Assert.AreEqual(2, modelsFromDb.Count);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateModel()
        {
            //Arrange
            var model = new Model("Asics", "E345", "Trail", 95.00m, "Women", "Test description");
            
            //Act
            await _modelsContext.CreateAsync(model);
            model.Price = 100.00m;
            await _modelsContext.UpdateAsync(model);
            var modelFromDb = await _modelsContext.ReadAsync(model.Id);

            //Assert
            Assert.AreEqual(100.00m, modelFromDb.Price);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveModel()
        {
            //Arrange
            var model = new Model("NewBalance", "G901", "Sport", 75.00m, "Unisex", "Test description");

            //Act
            await _modelsContext.CreateAsync(model);
            await _modelsContext.DeleteAsync(model.Id);
            var modelFromDb = await _modelsContext.ReadAsync(model.Id);

            //Assert
            Assert.IsNull(modelFromDb);
        }

    }
}
