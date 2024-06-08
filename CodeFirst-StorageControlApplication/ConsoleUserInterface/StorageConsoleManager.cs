using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer;
using BusinessLayer;

namespace ConsoleUserInterface
{
    public static class StorageConsoleManager
    {

        private static IModelService _modelService;

        public static void Initialize(IModelService modelService)
        {
            _modelService = modelService;
        }

        public static async Task AddShoeModel()
        {
            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Enter Code: ");
            string code = Console.ReadLine();

            Console.Write("Enter Shoe Type: ");
            string shoeType = Console.ReadLine();

            Console.Write("Enter Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                return;
            }

            Console.Write("Enter Gender Category (Men, Women, Unisex): ");
            string genderCategory = Console.ReadLine();

            Console.Write("Enter Description (optional): ");
            string description = Console.ReadLine();
            
            Model newModel;
            if (string.IsNullOrWhiteSpace(description))
            {
                newModel = new Model(brand, code, shoeType, price, genderCategory);
            }
            else
            {
                newModel = new Model(brand, code, shoeType, price, genderCategory, description);
            }

            // Optional: Add shoes if you want to include them in the model
            List<Shoe> shoes = new List<Shoe>();
            bool addShoes = true;

            while (addShoes)
            {
                Console.Write("Do you want to add a shoe to this model? (y/n): ");
                string response = Console.ReadLine().ToLower();

                if (response == "y")
                {
                    // Add shoe details (example placeholder code)
                    // You can prompt for specific shoe details here
                    Console.Write("Enter Shoe Size: ");
                    if (!int.TryParse(Console.ReadLine(), out int size))
                    {
                        Console.WriteLine("Invalid size. Please enter a valid integer.");
                        continue;
                    }

                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());

                    // Assume you have a constructor for Shoe that takes size and color
                    var newShoe = new Shoe(size, quantity, newModel);
                    shoes.Add(newShoe);
                }
                else
                {
                    addShoes = false;
                }
            }

            newModel.Shoes = shoes;



            await _modelService.CreateModelAsync(newModel);
            Console.WriteLine("Shoe model added successfully.");
        }

        public static async Task ViewAvailableShoeModels()
        {
            var availableModels = await _modelService.GetAllModelsAsync();

            Console.WriteLine("Available Shoe Models:");
            foreach (var model in availableModels)
            {
                Console.WriteLine($"ID: {model.Id}, Brand: {model.Brand}, Code: {model.Code}, Shoe Type: {model.ShoeType}, Price: {model.Price}, Gender: {model.GenderCategory}");
            }
        }

        public static async Task UpdateShoeModel()
        {
            Console.Write("Enter the ID of the shoe model to update: ");
            if (int.TryParse(Console.ReadLine(), out int modelId))
            {
                var modelToUpdate = await _modelService.GetModelByIdAsync(modelId);

                if (modelToUpdate != null)
                {
                    Console.WriteLine("Current details of the shoe model:");
                    Console.WriteLine($"ID: {modelToUpdate.Id}");
                    Console.WriteLine($"Brand: {modelToUpdate.Brand}");
                    Console.WriteLine($"Code: {modelToUpdate.Code}");
                    Console.WriteLine($"Shoe Type: {modelToUpdate.ShoeType}");
                    Console.WriteLine($"Price: {modelToUpdate.Price}");
                    Console.WriteLine($"Gender Category: {modelToUpdate.GenderCategory}");
                    Console.WriteLine($"Description: {modelToUpdate.Description}");

                    bool continueEditing = true;
                    while (continueEditing)
                    {
                        Console.WriteLine("Select which property to update:");
                        Console.WriteLine("1. Brand");
                        Console.WriteLine("2. Code");
                        Console.WriteLine("3. Shoe Type");
                        Console.WriteLine("4. Price");
                        Console.WriteLine("5. Gender Category");
                        Console.WriteLine("6. Finish Editing");

                        Console.Write("Enter your choice: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Console.Write("Enter new brand: ");
                                modelToUpdate.Brand = Console.ReadLine();
                                break;
                            case "2":
                                Console.Write("Enter new code: ");
                                modelToUpdate.Code = Console.ReadLine();
                                break;
                            case "3":
                                Console.Write("Enter new shoe type: ");
                                modelToUpdate.ShoeType = Console.ReadLine();
                                break;
                            case "4":
                                Console.Write("Enter new price: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                                {
                                    modelToUpdate.Price = price;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid price.");
                                }
                                break;
                            case "5":
                                Console.Write("Enter new gender category (Men, Women, Unisex): ");
                                modelToUpdate.GenderCategory = Console.ReadLine();
                                break;
                            case "6":
                                continueEditing = false;
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }

                    await _modelService.UpdateModelAsync(modelToUpdate);
                    Console.WriteLine("Shoe model updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Shoe model with ID {modelId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid model ID.");
            }
        }

        public static async Task RemoveShoeModel()
        {
            Console.Write("Enter the ID of the shoe model to remove: ");
            if (int.TryParse(Console.ReadLine(), out int modelId))
            {
                var modelToDelete = await _modelService.GetModelByIdAsync(modelId);
                if (modelToDelete != null)
                {
                    Console.WriteLine("Model found:");
                    Console.WriteLine("Current details of the shoe model:");
                    Console.WriteLine($"ID: {modelToDelete.Id}");
                    Console.WriteLine($"Brand: {modelToDelete.Brand}");
                    Console.WriteLine($"Code: {modelToDelete.Code}");
                    Console.WriteLine($"Shoe Type: {modelToDelete.ShoeType}");
                    Console.WriteLine($"Price: {modelToDelete.Price}");
                    Console.WriteLine($"Gender Category: {modelToDelete.GenderCategory}");
                    Console.WriteLine($"Description: {modelToDelete.Description}");

                    Console.Write("Are you sure you want to delete this model? (y/n): ");
                    var confirmation = Console.ReadLine();
                    if (confirmation.ToLower() == "y")
                    {
                        await _modelService.DeleteModelAsync(modelId);
                        Console.WriteLine("Shoe model deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine($"Shoe model with ID {modelId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid model ID.");
            }
        }

        public static void AddShoes()
        {
            // Code to add shoes
            Console.WriteLine("Adding shoes...");
            // Implementation details here
        }

        public static void ViewShoesOfParticularModel()
        {
            // Code to view shoes of a particular model
            Console.WriteLine("Viewing shoes of a particular model...");
            // Implementation details here
        }

        public static void ManageCustomers()
        {
            // Code to manage customers
            Console.WriteLine("Managing customers...");
            // Implementation details here
        }

        public static void MakePurchase()
        {
            // Code to make a purchase
            Console.WriteLine("Making a purchase...");
            // Implementation details here
        }

        public static void ViewPurchaseHistory()
        {
            // Code to view purchase history
            Console.WriteLine("Viewing purchase history...");
            // Implementation details here
        }

        public static async Task DiscardShoes()
        {
            Console.Write("Enter the Model ID of the shoes to discard: ");
            if (int.TryParse(Console.ReadLine(), out int modelId))
            {
                Console.Write("Enter the quantity of shoes to discard: ");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    //await _shoesContext.DiscardShoesAsync(modelId, quantity);
                    Console.WriteLine($"{quantity} shoes of Model ID {modelId} have been discarded.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Model ID.");
            }
        }
    }
}
