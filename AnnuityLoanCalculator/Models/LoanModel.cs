using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AnnuityLoanCalculator.Models
{
    public class LoanModel
    {
        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d{0,2}$", ErrorMessage = "Must be a currency number.")]
        [Display(Name = "Loan Sum ($)")]
        public double LoanSum { get; set; }

        [Required]
        [RegularExpression("\\d*$", ErrorMessage = "Must be an integer.")]
        [Display(Name = "Loan Term (months)")]
        public int LoanTerm { get; set; }

        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d*$", ErrorMessage = "Only floating point numbers allowed.")]
        [Display(Name = "Loan Interest (%)")]
        public double LoanInterest { get; set; }

        [Required]
        [RegularExpression("\\d*$", ErrorMessage = "Must be an integer.")]
        [Display(Name = "Payment Step (n)")]
        public int PaymentStep { get; set; }
    }
}