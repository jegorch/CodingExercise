using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuestionText { get; set; }

        // Test data
        public static KeyValuePair<int,string> Age = new KeyValuePair<int, string>(1,"Age");
        public static KeyValuePair<int,string> Student = new KeyValuePair<int, string>(2, "Is Student");
        public static KeyValuePair<int,string> Income = new KeyValuePair<int, string>(3,"Income");
    }
}
