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
        private static IClientService _clientService;
        private static IShoeService _shoeService;
        private static IOrderService _orderService;

        public static void Initialize(IModelService modelService, IClientService clientService, IShoeService shoeService, IOrderService orderService)
        {
            _modelService = modelService;
            _clientService = clientService;
            _shoeService = shoeService;
            _orderService = orderService;
        }

        public static async Task AddShoeModel()
        {
            Console.Write("Enter Brand:");
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
            PrintOption("Shoe model added successfully.", true);
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
                    PrintOption("Shoe model updated successfully.", true);
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
                        PrintOption("Shoe model deleted successfully.", true);
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

        public static async Task AddShoes()
        {
            Console.Write("Enter the ID of the shoe model to add shoes: ");
            if (int.TryParse(Console.ReadLine(), out int modelId))
            {
                var modelToAddShoes = await _modelService.GetModelByIdAsync(modelId, true);

                if (modelToAddShoes != null)
                {
                    Console.WriteLine("Current details of the shoe model:");
                    Console.WriteLine($"ID: {modelToAddShoes.Id}");
                    Console.WriteLine($"Brand: {modelToAddShoes.Brand}");
                    Console.WriteLine($"Code: {modelToAddShoes.Code}");
                    Console.WriteLine($"Shoe Type: {modelToAddShoes.ShoeType}");
                    Console.WriteLine($"Price: {modelToAddShoes.Price}");
                    Console.WriteLine($"Gender Category: {modelToAddShoes.GenderCategory}");
                    Console.WriteLine($"Description: {modelToAddShoes.Description}");
                    Console.WriteLine();

                    bool continueAddingShoes = true;
                    while (continueAddingShoes)
                    {
                        Console.Write("Do you want to add a shoe to this model? (y/n): ");
                        string response = Console.ReadLine().ToLower();

                        if (response == "y")
                        {
                            Console.Write("Enter Shoe Size: ");
                            if (!int.TryParse(Console.ReadLine(), out int size))
                            {
                                Console.WriteLine("Invalid size. Please enter a valid integer.");
                                continue;
                            }

                            Console.Write("Enter Quantity: ");
                            if (!int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                Console.WriteLine("Invalid quantity. Please enter a valid integer.");
                                continue;
                            }
                            bool isExisting = false;
                            //var existingShoe = modelToAddShoes.Shoes.FirstOrDefault(s => s.Size == size);
                            var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                            var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);

                            if (existingShoe != null)
                            {
                                existingShoe.Quantity += quantity;
                                await _shoeService.UpdateShoeAsync(existingShoe);
                            }
                            else
                            {
                                var newShoe = new Shoe(size, quantity, modelToAddShoes);
                                await _shoeService.CreateShoeAsync(newShoe);
                                modelToAddShoes.Shoes.Add(newShoe);
                            }


                            //if (existingShoe != null)
                            //{
                            //    existingShoe.Quantity += quantity;
                            //    await _shoeService.UpdateShoeAsync(existingShoe);
                            //}
                            //else
                            //{
                            //    var newShoe = new Shoe(size, quantity, modelToAddShoes);
                            //    modelToAddShoes.Shoes.Add(newShoe);
                            //    await _shoeService.CreateShoeAsync(newShoe);
                            //}
                        }
                        else
                        {
                            continueAddingShoes = false;
                        }
                    }

                    // Update the model in the database
                    await _modelService.UpdateModelAsync(modelToAddShoes, true);
                    PrintOption("Shoes added successfully.", true);
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

        public static async Task ViewShoesOfParticularModel()
        {
            Console.Write("Enter the model ID to view its shoes: ");
            if (int.TryParse(Console.ReadLine(), out int modelId))
            {
                var model = await _modelService.GetModelByIdAsync(modelId, true);
                if (model != null)
                {
                    Console.WriteLine($"Model ID: {model.Id}");
                    Console.WriteLine($"Brand: {model.Brand}");
                    Console.WriteLine($"Code: {model.Code}");
                    Console.WriteLine($"Shoe Type: {model.ShoeType}");
                    Console.WriteLine($"Price: {model.Price}");
                    Console.WriteLine($"Gender Category: {model.GenderCategory}");
                    Console.WriteLine($"Description: {model.Description}");
                    Console.WriteLine("Shoes:");

                    foreach (var shoe in model.Shoes)
                    {
                        Console.WriteLine($"  Shoe ID: {shoe.Id}");
                        Console.WriteLine($"  Size: {shoe.Size}");
                        Console.WriteLine($"  Quantity: {shoe.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine("Model not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid model ID.");
            }
        }

        public static async Task ManageCustomers()
        {
            bool manageCustomersRunning = true;

            while (manageCustomersRunning)
            {
                Console.Clear();
                Console.WriteLine("Customer Management");
                Console.WriteLine("1. Add New Customer");
                Console.WriteLine("2. Find Customer by ID");
                Console.WriteLine("3. View All Customers");
                Console.WriteLine("4. Update Customer");
                Console.WriteLine("5. Remove Customer");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddNewCustomer();
                        break;
                    case "2":
                        await FindCustomerById();
                        break;
                    case "3":
                        await ViewAllCustomers();
                        break;
                    case "4":
                        await UpdateCustomer();
                        break;
                    case "5":
                        await RemoveCustomer();
                        break;
                    case "0":
                        manageCustomersRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (manageCustomersRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }


            public static async Task ManagePurchase()
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create Purchase");
                Console.WriteLine("2. Complete Purchase");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            await CreatePurchase();
                            break;
                        case 2:
                            await CompletePurchase();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            public static async Task ViewPurchasesHistory()
            {

                var orders = await _orderService.GetAllPurchasesHistoryAsync();
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Order Date: {order.OrderDate}, Client ID: {order.ClientId}");
                    foreach (var detail in order.OrderDetails)
                    {
                        Console.WriteLine($"Shoe ID: {detail.ShoeId}, Size: {detail.Shoe.Size}, Quantity: {detail.Quantity}");
                    }
                }


            }

            public static async Task DiscardShoes()
            {
                Console.Write("Enter Shoe Model ID: ");
                if (!int.TryParse(Console.ReadLine(), out int modelId))
                {
                    Console.WriteLine("Invalid Shoe Model ID.");
                    return;
                }

                var model = await _modelService.GetModelByIdAsync(modelId);
                if (model == null)
                {
                    Console.WriteLine($"Shoe model with ID {modelId} not found.");
                    return;
                }

                Console.Write("Enter Shoe Size: ");
                if (!double.TryParse(Console.ReadLine(), out double size))
                {
                    Console.WriteLine("Invalid size. Please enter a valid number.");
                    return;
                }

                Console.Write("Enter Quantity to discard: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid Quantity. Please enter a positive integer.");
                    return;
                }

                var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);
                if (existingShoe == null)
                {
                    Console.WriteLine($"No shoe found with size {size} for model ID {modelId}.");
                    return;
                }

                if (existingShoe.Quantity < quantity)
                {
                    Console.WriteLine($"Not enough quantity for shoe ID {existingShoe.Id} with size {size}. Available: {existingShoe.Quantity}, Requested: {quantity}");
                    return;
                }

                existingShoe.Quantity -= quantity;
                await _shoeService.UpdateShoeAsync(existingShoe);
                Console.WriteLine($"Discarded {quantity} of shoe ID {existingShoe.Id} with size {size}. Remaining quantity: {existingShoe.Quantity}");
        
            }

        

        //private methods for ManageCustomers()
        private static async Task AddNewCustomer()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer address: ");
            string address = Console.ReadLine();

            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();

            Console.Write("Enter customer phone number: ");
            string phoneNumber = Console.ReadLine();

            var newClient = new Client(name, address, email, phoneNumber);

            await _clientService.CreateClientAsync(newClient);
            PrintOption("Customer added successfully.", true);
        }

        private static async Task FindCustomerById()
        {
            Console.Write("Enter the ID of the customer to find: ");
            if (int.TryParse(Console.ReadLine(), out int clientId))
            {
                var client = await _clientService.GetClientByIdAsync(clientId);
                if (client != null)
                {
                    Console.WriteLine($"Client ID: {client.Id}");
                    Console.WriteLine($"Name: {client.Name}");
                    Console.WriteLine($"Address: {client.Address}");
                    Console.WriteLine($"Email: {client.Email}");
                    Console.WriteLine($"Phone Number: {client.PhoneNumber}");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        private static async Task ViewAllCustomers()
        {
            var clients = await _clientService.GetAllClientsAsync();

            if (clients.Count > 0)
            {
                foreach (var client in clients)
                {
                    Console.WriteLine($"Client ID: {client.Id}");
                    Console.WriteLine($"Name: {client.Name}");
                    Console.WriteLine($"Address: {client.Address}");
                    Console.WriteLine($"Email: {client.Email}");
                    Console.WriteLine($"Phone Number: {client.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("No customers found.");
            }
        }

        private static async Task UpdateCustomer()
        {
            Console.Write("Enter the ID of the customer to update: ");
            if (int.TryParse(Console.ReadLine(), out int clientId))
            {
                var client = await _clientService.GetClientByIdAsync(clientId);

                if (client != null)
                {
                    bool updating = true;
                    while (updating)
                    {
                        Console.Clear();
                        Console.WriteLine("Select the property to update:");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Address");
                        Console.WriteLine("3. Email");
                        Console.WriteLine("4. Phone Number");
                        Console.WriteLine("0. Finish updating");

                        Console.Write("Enter your choice: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Console.Write("Enter new name (leave empty to keep current): ");
                                string newName = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newName))
                                {
                                    client.Name = newName;
                                }
                                break;
                            case "2":
                                Console.Write("Enter new address (leave empty to keep current): ");
                                string newAddress = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newAddress))
                                {
                                    client.Address = newAddress;
                                }
                                break;
                            case "3":
                                Console.Write("Enter new email (leave empty to keep current): ");
                                string newEmail = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newEmail))
                                {
                                    client.Email = newEmail;
                                }
                                break;
                            case "4":
                                Console.Write("Enter new phone number (leave empty to keep current): ");
                                string newPhoneNumber = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newPhoneNumber))
                                {
                                    client.PhoneNumber = newPhoneNumber;
                                }
                                break;
                            case "0":
                                updating = false;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }

                    await _clientService.UpdateClientAsync(client);
                    PrintOption("Customer updated successfully.", true);

                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        private static async Task RemoveCustomer()
        {
            Console.Write("Enter the ID of the customer to remove: ");
            if (int.TryParse(Console.ReadLine(), out int clientId))
            {
                await _clientService.DeleteClientAsync(clientId);
                PrintOption("Customer removed successfully.", true);
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }



        //private methods for ManagePurchase

        private static async Task CreatePurchase()
        {
            Console.Write("Enter Client ID: ");
            if (!int.TryParse(Console.ReadLine(), out int clientId))
            {
                Console.WriteLine("Invalid Client ID.");
                return;
            }

            var client = await _clientService.GetClientByIdAsync(clientId);
            if (client == null)
            {
                Console.WriteLine("Client not found.");
                return;
            }

            Console.Write("Do you want to use the current date and time for the order? (y/n): ");
            string response = Console.ReadLine().ToLower();

            DateTime orderDate;
            if (response == "y")
            {
                orderDate = DateTime.Now;
            }
            else
            {
                Console.Write("Enter desired order date and time (yyyy-MM-dd HH:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out orderDate))
                {
                    Console.WriteLine("Invalid date format. Using current date and time.");
                    orderDate = DateTime.Now;
                }
            }

            var order = new Order(orderDate, client);
            await _orderService.MakePurchaseAsync(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            bool continueAddingDetails = true;

            while (continueAddingDetails)
            {
                Console.Write("Enter Shoe Model ID: ");
                if (!int.TryParse(Console.ReadLine(), out int modelId))
                {
                    Console.WriteLine("Invalid Shoe Model ID.");
                    continue;
                }

                var model = await _modelService.GetModelByIdAsync(modelId);
                if (model == null)
                {
                    Console.WriteLine($"Shoe model with ID {modelId} not found.");
                    continue;
                }

                Console.Write("Enter Shoe Size: ");
                if (!double.TryParse(Console.ReadLine(), out double size))
                {
                    Console.WriteLine("Invalid size. Please enter a valid number.");
                    continue;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                    continue;
                }

                var shoesForModel = await _shoeService.GetShoesByModelIdAsync(modelId);
                var existingShoe = shoesForModel.FirstOrDefault(s => s.Size == size);
                if (existingShoe == null)
                {
                    Console.WriteLine($"No shoe found with size {size} for model ID {modelId}.");
                    continue;
                }

                var orderDetail = new OrderDetail(quantity, existingShoe, order);
                orderDetails.Add(orderDetail);

                Console.Write("Do you want to add another shoe to the order? (y/n): ");
                response = Console.ReadLine().ToLower();
                continueAddingDetails = response == "y";
            }

            if (orderDetails.Count == 0)
            {
                Console.WriteLine("No order details added.");
                return;
            }

            order.OrderDetails = orderDetails;
            await _orderService.UpdateOrderAsync(order);
            PrintOption("Order created successfully.", true);
        }

        private static async Task CompletePurchase()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order ID.");
                return;
            }

            var order = await _orderService.GetPurchaseByIdAsync(orderId, true);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }

            foreach (var detail in order.OrderDetails)
            {
                var shoe = await _shoeService.GetShoeByIdAsync(detail.ShoeId);
                detail.Shoe = shoe;

                if (shoe.Quantity < detail.Quantity)
                {
                    Console.WriteLine($"Not enough quantity for shoe ID {shoe.Id} with size {shoe.Size}. Available: {shoe.Quantity}, Requested: {detail.Quantity}");
                    return;
                }

                shoe.Quantity -= detail.Quantity;
                await _shoeService.UpdateShoeAsync(shoe);
            }

            PrintOption("Purchase completed successfully.", true);
        }



        /// <summary>
        /// Shows a message in a frame with specified color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isSuccess"></param>
        static void PrintOption(string message, bool isSuccess = false)
        {
            if (isSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green; // sets the color to green
            }
            else
            {
                Console.ResetColor(); // sets the color to white
            }

            Console.WriteLine("╔═════════════════════════════════════════════╗");
            Console.WriteLine($"║ {message.PadRight(43)} ║");
            Console.WriteLine("╚═════════════════════════════════════════════╝");

            Console.ResetColor(); // sets the color back to default
        }
    }   
    
}
