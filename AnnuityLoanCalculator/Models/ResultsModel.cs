using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AnnuityLoanCalculator.Models
{
    public class ResultsModel
    {
        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d*$", ErrorMessage = "Must be a floating point number.")]
        [Display(Name = "Payment Number")]
        public double PaymentNumber { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        public string PaymentDate { get; set; }

        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d*$", ErrorMessage = "Must be a floating point number.")]
        [Display(Name = "Principal Payment")]
        public double PrincipalPayment { get; set; }

        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d*$", ErrorMessage = "Must be a floating point number.")]
        [Display(Name = "Interest Payment")]
        public double InterestPayment { get; set; }

        [Required]
        [RegularExpression("^-?\\d*[.,]?\\d*$", ErrorMessage = "Must be a floating point number.")]
        [Display(Name = "Principal Balance")]
        public double PrincipalBalance { get; set; }

    }
}