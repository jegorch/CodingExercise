using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }

        public virtual ICollection<CustomerSurvey> CustomerSurvey { get; set; }

        
        //public AgeEnum Age { get; set; }
        //public bool IsStudent { get; set; }
        //public IncomeEnum Income { get; set; }
    }
}
