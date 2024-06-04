namespace PresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║    Storage Control Application    ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║ 1. Add New Shoe Model             ║");
                Console.WriteLine("║ 2. View Available Shoe Models     ║");
                Console.WriteLine("║ 3. Make a Purchase                ║");
                Console.WriteLine("║ 4. View Purchase History          ║");
                Console.WriteLine("║ 5. Manage Customers               ║");
                Console.WriteLine("║ 0. Exit                           ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add New Shoe Model selected.");
                        // AddNewShoeModel();
                        break;
                    case "2":
                        Console.WriteLine("View Available Shoe Models selected.");
                        // ViewAvailableShoeModels();
                        break;
                    case "3":
                        Console.WriteLine("Make a Purchase selected.");
                        // MakePurchase();
                        break;
                    case "4":
                        Console.WriteLine("View Purchase History selected.");
                        // ViewPurchaseHistory();
                        break;
                    case "5":
                        Console.WriteLine("Manage Customers selected.");
                        // ManageCustomers();
                        break;
                    case "0":
                        Console.WriteLine("Exiting the program...");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
