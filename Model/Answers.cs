using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodingExercise.Model
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int SelectedAnswerId { get; set; }
        public virtual PossibleAnswers SelectedAnswer { get; set; }
    }
}
