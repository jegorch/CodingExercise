using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class Product
    {
        public Product()
        {
            Rules = new HashSet<ProductRules>();
            Bundles = new HashSet<ProductBundle>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ProductName { get; set; }
        public virtual ICollection<ProductRules> Rules { get; set; }
        public virtual ICollection<ProductBundle> Bundles { get; set; }

        // Test data
        public static KeyValuePair<int, string> CurrentAccount = new KeyValuePair<int, string>(1, "Current Account");
        public static KeyValuePair<int, string> CurrentAccountPlus = new KeyValuePair<int, string>(2, "Current Account Plus");
        public static KeyValuePair<int, string> JuniorSaverAccount = new KeyValuePair<int, string>(3, "Junior Saver Account");
        public static KeyValuePair<int, string> StudentAccount = new KeyValuePair<int, string>(4, "Student Account");
        public static KeyValuePair<int, string> DebitCard = new KeyValuePair<int, string>(5, "Debit Card");
        public static KeyValuePair<int, string> CreditCard = new KeyValuePair<int, string>(6, "Credit Card");
        public static KeyValuePair<int, string> GoldCreditCard = new KeyValuePair<int, string>(7, "Gold Credit Card");
    }
}
