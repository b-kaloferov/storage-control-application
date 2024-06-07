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

        public async static void AddShoeModel()
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

        public static void ViewAvailableShoeModels()
        {
            // Code to view available shoe models
            Console.WriteLine("Viewing available shoe models...");
            // Implementation details here
        }

        public static void UpdateShoeModel()
        {
            // Code to update a shoe model
            Console.WriteLine("Updating a shoe model...");
            // Implementation details here
        }

        public static void RemoveShoeModel()
        {
            // Code to remove a shoe model
            Console.WriteLine("Removing a shoe model...");
            // Implementation details here
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
