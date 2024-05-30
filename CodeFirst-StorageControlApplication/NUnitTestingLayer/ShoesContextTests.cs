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
    public class ShoesContextTests
    {
        private StorageDbContext _dbContext;
        private ShoesContext _shoesContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StorageDbContext>()
                .UseInMemoryDatabase(databaseName: "ShoesTestDb")
                .Options;

            _dbContext = new StorageDbContext(options);
            _shoesContext = new ShoesContext(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddShoeToDatabase()
        {
            // Arrange
            var model = new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description");
            _dbContext.Models.Add(model);
            await _dbContext.SaveChangesAsync();

            var shoe = new Shoe(42, 10, model);

            // Act
            await _shoesContext.CreateAsync(shoe);

            // Assert
            var shoeFromDb = await _dbContext.Shoes.Include(s => s.Model).FirstOrDefaultAsync(s => s.Id == shoe.Id);
            Assert.IsNotNull(shoeFromDb, "Shoe was not added to the database.");
            Assert.AreEqual(shoe.Size, shoeFromDb.Size);
            Assert.AreEqual(shoe.Quantity, shoeFromDb.Quantity);
            Assert.AreEqual(shoe.Model.Id, shoeFromDb.Model.Id);
            Assert.AreEqual(shoe.Model.Brand, shoeFromDb.Model.Brand);
        }

        [Test]
        public async Task ReadAsync_ShouldReturnShoeFromDatabase()
        {
            // Arrange
            var model = new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description");
            _dbContext.Models.Add(model);
            await _dbContext.SaveChangesAsync();

            var shoe = new Shoe(42, 10, model);
            _dbContext.Shoes.Add(shoe);
            await _dbContext.SaveChangesAsync();

            // Act
            var shoeFromDb = await _shoesContext.ReadAsync(shoe.Id, useNavigationalProperties: true);

            // Assert
            Assert.IsNotNull(shoeFromDb, "Shoe was not found in the database.");
            Assert.AreEqual(shoe.Size, shoeFromDb.Size);
            Assert.AreEqual(shoe.Quantity, shoeFromDb.Quantity);
            Assert.AreEqual(shoe.Model.Id, shoeFromDb.Model.Id);
        }

        [Test]
        public async Task ReadAllAsync_ShouldReturnAllShoesFromDatabase()
        {
            // Arrange
            var model = new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description");
            _dbContext.Models.Add(model);
            await _dbContext.SaveChangesAsync();

            var shoes = new List<Shoe>
            {
                new Shoe(42, 10, model),
                new Shoe(43, 5, model),
                new Shoe(44, 2, model)
            };

            _dbContext.Shoes.AddRange(shoes);
            await _dbContext.SaveChangesAsync();

            // Act
            var shoesFromDb = await _shoesContext.ReadAllAsync();

            // Assert
            Assert.AreEqual(shoes.Count, shoesFromDb.Count, "The number of shoes returned does not match the number of shoes added.");
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateShoeInDatabase()
        {
            // Arrange
            var model = new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description");
            _dbContext.Models.Add(model);
            await _dbContext.SaveChangesAsync();

            var shoe = new Shoe(42, 10, model);
            _dbContext.Shoes.Add(shoe);
            await _dbContext.SaveChangesAsync();

            var updatedModel = new Model("UpdatedBrand", "UpdatedCode", "Boots", 149.99m, "Women", "Updated description");
            _dbContext.Models.Add(updatedModel);
            await _dbContext.SaveChangesAsync();

            shoe.Size = 43;
            shoe.Quantity = 20;
            shoe.Model = updatedModel;
            shoe.ModelId = updatedModel.Id;

            // Act
            await _shoesContext.UpdateAsync(shoe);

            // Assert
            var shoeFromDb = await _dbContext.Shoes.Include(s => s.Model).FirstOrDefaultAsync(s => s.Id == shoe.Id);
            Assert.IsNotNull(shoeFromDb, "Shoe was not found in the database.");
            Assert.AreEqual(shoe.Size, shoeFromDb.Size);
            Assert.AreEqual(shoe.Quantity, shoeFromDb.Quantity);
            Assert.AreEqual(shoe.Model.Id, shoeFromDb.Model.Id);
            Assert.AreEqual(shoe.Model.Brand, shoeFromDb.Model.Brand);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveShoeFromDatabase()
        {
            // Arrange
            var model = new Model("TestBrand", "Code123", "Sneakers", 99.99m, "Men", "Test description");
            _dbContext.Models.Add(model);
            await _dbContext.SaveChangesAsync();

            var shoe = new Shoe(42, 10, model);
            _dbContext.Shoes.Add(shoe);
            await _dbContext.SaveChangesAsync();

            // Act
            var shoeToDelete = await _dbContext.Shoes.AsNoTracking().FirstOrDefaultAsync(s => s.Id == shoe.Id);
            await _shoesContext.DeleteAsync(shoeToDelete.Id);

            // Assert
            var shoeFromDb = await _dbContext.Shoes.FindAsync(shoe.Id);
            Assert.IsNull(shoeFromDb, "Shoe was not removed from the database.");
        }
    }
}

