using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class Shoe
    {
        [Key]
        public int Id { get; set; }

        [Range(20, 50, ErrorMessage = "The size must be between 20 and 50.")]
        public double Size { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The quantity must be positive number.")]
        public int Quantity { get; set; }

        [Required]
        public Model Model { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }

        private Shoe() 
        {
            
        }

        public Shoe(double size, int quantity, Model model)
        {
            Size = size;
            Quantity = quantity;
            Model = model;
            ModelId = model.Id;
        }
    }
}
