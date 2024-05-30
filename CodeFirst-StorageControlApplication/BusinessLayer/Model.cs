using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public enum ShoeGender
    {
        Men,
        Women,
        Unisex
    }

    public class CaseInsensitiveEnumAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are considered valid
            }

            string stringValue = value.ToString();

            foreach (var enumValue in Enum.GetValues(validationContext.ObjectType))
            {
                if (stringValue.Equals(enumValue.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"The value '{value}' is not a valid {validationContext.DisplayName}.");
        }
    }

    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The brand cannot be more than 200 symbols.")]
        public string Brand { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The model code cannot be more than 20 symbols.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The shoe type cannot be more than 50 symbols.")]
        public string ShoeType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The price must be positive.")]
        public decimal Price { get; set; }

        [Required]
        [CaseInsensitiveEnum]  // provides case-insensitive validation (the value can only be "men", "women" or "unisex")
        public string GenderCategory { get; set; }

        [MaxLength(1000, ErrorMessage = "The description cannot be more than 1000 symbols.")]
        public string Description { get; set; }

        public List<Shoe> Shoes { get; set; }

        private Model() 
        {
            Shoes = new List<Shoe>();
        }

        public Model(string brand, string code, string shoeType, decimal price, string genderCategory) : this()
        {
            Brand = brand;
            Code = code;
            ShoeType = shoeType;
            Price = price;
            GenderCategory = genderCategory;
        }

        public Model(string brand, string code, string shoeType, decimal price, string genderCategory, List<Shoe> shoes)
        {
            Brand = brand;
            Code = code;
            ShoeType = shoeType;
            Price = price;
            GenderCategory = genderCategory;
            Shoes = shoes;
        }

        public Model(string brand, string code, string shoeType, decimal price, string genderCategory, string description) : this()
        {
            Brand = brand;
            Code = code;
            ShoeType = shoeType;
            Price = price;
            GenderCategory = genderCategory;
            Description = description;
        }

        public Model(string brand, string code, string shoeType, decimal price, string genderCategory, string description, List<Shoe> shoes)
        {
            Brand = brand;
            Code = code;
            ShoeType = shoeType;
            Price = price;
            GenderCategory = genderCategory;
            Description = description;
            Shoes = shoes;
        }
    }
}
