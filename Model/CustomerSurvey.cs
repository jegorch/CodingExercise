using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Model
{
    public class CustomerSurvey
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public DateTime Date { get; set; }
    }
}
