using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Model
{
    public class SurveyQuestion
    {
        [Required]
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
