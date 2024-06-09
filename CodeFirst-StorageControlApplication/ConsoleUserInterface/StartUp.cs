using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace ConsoleUserInterface
{
    internal class StartUp
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StorageDbContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-UNHGGLSQ\\SQLEXPRESS;Database=StorageDb;Trusted_Connection=True;TrustServerCertificate=True;");
            using (var storageDbContext = new StorageDbContext(optionsBuilder.Options))
            {
                var modelsContext = new ModelsContext(storageDbContext);

                var modelService = new ModelService(modelsContext);

                StorageConsoleManager.Initialize(modelService);
                bool isRunning = true;

                while (isRunning)
                {
                    Console.Clear();
                    DrawMenu();

                    Console.Write("╔═════════════════════════════════════════════╗\n");
                    Console.Write("║ Select an option:                           ║");
                    Console.SetCursorPosition(Console.CursorLeft - 27, Console.CursorTop); // Move cursor to correct position
                    Console.ForegroundColor = ConsoleColor.Blue; // sets the color to blue

                    string choice = Console.ReadLine();
                    Console.ResetColor(); // sets the color back to white
                    Console.WriteLine("╚═════════════════════════════════════════════╝");

                    Console.Clear();
                    DrawMenu(choice);

                    // Perform the selected action
                    switch (choice)
                    {
                        case "1":
                            PrintOption("Add New Shoe Model selected.");
                            await StorageConsoleManager.AddShoeModel();
                            break;
                        case "2":
                            PrintOption("View Available Shoe Models selected.");
                            await StorageConsoleManager.ViewAvailableShoeModels();
                            break;
                        case "3":
                            PrintOption("Update a Shoe Model selected.");
                            await StorageConsoleManager.UpdateShoeModel();
                            break;
                        case "4":
                            PrintOption("Remove a Shoe Model selected.");
                            await StorageConsoleManager.RemoveShoeModel();
                            break;
                        case "5":
                            PrintOption("Add Shoes selected.");
                            await StorageConsoleManager.AddShoes();
                            break;
                        case "6":
                            PrintOption("View Shoes of a Particular Model selected.");
                            // StorageConsoleManager.ViewShoesOfParticularModel();
                            break;
                        case "7":
                            PrintOption("Manage Customers selected.");
                            // StorageConsoleManager.ManageCustomers();
                            break;
                        case "8":
                            PrintOption("Make a Purchase selected.");
                            // StorageConsoleManager.MakePurchase();
                            break;
                        case "9":
                            PrintOption("View Purchase History selected.");
                            // StorageConsoleManager.ViewPurchaseHistory();
                            break;
                        case "10":
                            PrintOption("Discard Shoes selected.");
                            // StorageConsoleManager.DiscardShoes();
                            break;
                        case "0":
                            PrintOption("Exiting the program...");
                            isRunning = false;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed; // sets the color to dark red
                            PrintOption("Invalid choice. Please try again.");
                            Console.ResetColor();
                            break;
                    }

                    if (isRunning)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }

                }
            }
        }

        /// <summary>
        /// Draws the selected option in an appropriate color.
        /// </summary>
        /// <param name="selectedOption"></param>
        static void DrawMenu(string selectedOption = null)
        {
            Console.WriteLine("╔═════════════════════════════════════════════╗");
            Console.WriteLine("║         Storage Control Application         ║");
            Console.WriteLine("╚═════════════════════════════════════════════╝");
            Console.WriteLine("╔═════════════════════════════════════════════╗");

            // Options are stored in an array for easier access
            string[] options = {
                "1. Add New Shoe Model",
                "2. View Available Shoe Models",
                "3. Update a Shoe Model",
                "4. Remove a Shoe Model",
                "5. Add Shoes",
                "6. View Shoes of a Particular Model",
                "7. Manage Customers",
                "8. Make a Purchase",
                "9. View Purchase History",
                "10. Discard Shoes",
                "0. Exit"
            };

            foreach (var option in options)
            {
                // Checks if the current option is chosen
                if (option.StartsWith(selectedOption + "."))
                {
                    Console.Write("║ ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(option);
                    Console.ResetColor();
                    Console.WriteLine("".PadRight(43 - option.Length) + " ║"); // Adds the necessary number of spaces
                }
                else
                {
                    Console.WriteLine($"║ {option.PadRight(43)} ║");
                }
            }

            Console.WriteLine("╚═════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Shows a message in a frame.
        /// </summary>
        /// <param name="message"></param>
        static void PrintOption(string message)
        {
            Console.WriteLine("╔═════════════════════════════════════════════╗");
            Console.WriteLine($"║ {message.PadRight(43)} ║");
            Console.WriteLine("╚═════════════════════════════════════════════╝");
        }
    }
}
