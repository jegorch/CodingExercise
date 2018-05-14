using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodingExercise.Model
{
    public class BundleRules
    {
        public int BundleId { get; set; }
        public virtual Bundle Bundle { get; set; }
        public int PossibleAnswerId { get; set; }
        public virtual PossibleAnswers PossibleAnswer { get; set; }
    }
}
