using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class AccountType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //CurrentAccount,
        //CurrentAccountPlus,
        //StudentAccount,
        //PensionerAccount
        public string AccountName { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Bundle> Bundles { get; set; }

        // Test data
        public static KeyValuePair<int, string> CurrentAccount = new KeyValuePair<int, string>(1, "Current Account");
        public static KeyValuePair<int, string> CurrentAccountPlus = new KeyValuePair<int, string>(2, "Current Account Plus");
        public static KeyValuePair<int, string> StudentAccount = new KeyValuePair<int, string>(3, "Student Account");
        public static KeyValuePair<int, string> PensionerAccount = new KeyValuePair<int, string>(4, "Pensioner Account");

    }
}
