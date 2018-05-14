using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Model
{
    public class ProductBundle
    {
        [Required]
        public int BundleId { get; set; }
        public virtual Bundle Bundle { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
