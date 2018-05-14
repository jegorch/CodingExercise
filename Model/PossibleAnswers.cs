using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class PossibleAnswers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        /// <summary>
        /// Lable 
        /// </summary>
        public string Text { get; set; }

        public virtual ICollection<BundleRules> BundlePossibleAnswers { get; set; }
        public virtual ICollection<ProductRules> ProductPossibleAnswers { get; set; }


        //Test data
        /// <summary>
        /// id:"1"; Text:"0-17"
        /// </summary>
        public static KeyValuePair<int, string> Age_0_17 = new KeyValuePair<int, string>(1, "0-17");
        /// <summary>
        /// id:"2"; Text:"18-64"
        /// </summary>
        public static KeyValuePair<int, string> Age_18_64 = new KeyValuePair<int, string>(2, "18-64");
        /// <summary>
        /// id:"3"; Text:"65+"
        /// </summary>
        public static KeyValuePair<int, string> Age_65plus = new KeyValuePair<int, string>(3, "65+");

        /// <summary>
        /// id:"4"; Text:"Yes"
        /// </summary>
        public static KeyValuePair<int, string> Yes = new KeyValuePair<int, string>(4, "Yes");
        /// <summary>
        /// id:"5"; Text:"No"
        /// </summary>
        public static KeyValuePair<int, string> No = new KeyValuePair<int, string>(5, "No");


        /// <summary>
        /// id:"6"; Text:"0-17"
        /// </summary>
        public static KeyValuePair<int, string> Income_0 = new KeyValuePair<int, string>(6, "0");
        /// <summary>
        /// id:"7"; Text:"1-12000"
        /// </summary>
        public static KeyValuePair<int, string> Income_1_12000 = new KeyValuePair<int, string>(7, "1-12000");
        /// <summary>
        /// id:"8"; Text:"12001-40000"
        /// </summary>
        public static KeyValuePair<int, string> Income_12001_40000 = new KeyValuePair<int, string>(8, "12001-40000");
        /// <summary>
        /// id:"9"; Text:"40001+"
        /// </summary>
        public static KeyValuePair<int, string> Income_40001plus = new KeyValuePair<int, string>(9, "40001+");

    }
}
