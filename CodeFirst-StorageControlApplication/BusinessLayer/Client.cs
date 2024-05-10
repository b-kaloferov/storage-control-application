using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The name cannot be more than 100 symbols.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The address cannot be more than 200 symbols.")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\+\d{3}[-. ]?\d{3}[-. ]?\d{3}[-. ]?\d{3}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        public List<Order>Orders { get; set; }

        private Client()
        {
            Orders = new List<Order>();
        }
        public Client(string name, string address, string email, string phoneNumber) : this()
        {
            Name = name;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Client(string name, string address, string email, string phoneNumber, List<Order> orders)
        {
            Name = name;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            Orders = orders;
        }

    }
}
