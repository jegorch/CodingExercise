using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Model
{
    public class ProductRules
    {
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int PossibleAnswerId { get; set; }
        public virtual PossibleAnswers PossibleAnswer { get; set; }
    }
}
