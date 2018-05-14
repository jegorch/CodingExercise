using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class Bundle
    {
        public Bundle()
        {
            Rules = new HashSet<BundleRules>();
            ProductIncluded = new HashSet<ProductBundle>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BundleName { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<ProductBundle> ProductIncluded { get; set; }
        public virtual ICollection<BundleRules> Rules { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public int Value { get; set; }

        // Test data
        public static KeyValuePair<int, string> JuniorSaver = new KeyValuePair<int, string>(1, "Junior Saver");
        public static KeyValuePair<int, string> Student = new KeyValuePair<int, string>(2, "Student");
        public static KeyValuePair<int, string> Classic = new KeyValuePair<int, string>(3, "Classic");
        public static KeyValuePair<int, string> ClassicPlus = new KeyValuePair<int, string>(4, "Classic Plus");
        public static KeyValuePair<int, string> Gold = new KeyValuePair<int, string>(5, "Gold");
    }
}
