using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class Survey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Test data
        public static Tuple<int, string, string> FirstSurvey = new Tuple<int, string, string>(1, "First Survey", "Given answers to the questions, returns a recommended bundle");
    }
}
