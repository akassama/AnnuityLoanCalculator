using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnuityLoanCalculator.Models
{
    public class AppFunctions
    {

		//GetPrincipalPayment
		/// <summary>
		/// Gets the principal payment. Takes monthly payment, current balance, and interest rate
		/// </summary>
		/// <returns>PrincipalPayment</returns>
		public double GetPrincipalPayment(double monthly_payment, double current_loan_balance, double monthly_interest_rate)
		{
			double i = (monthly_interest_rate / 100) / 12;
			double PrincipalPayment = monthly_payment - (current_loan_balance * (i));
			return Double.Parse(String.Format("{0:.##}", PrincipalPayment));
		}

		//GetTotalMonthlyPayment
		/// <summary>
		/// Gets the total monthly payment. x = (P * i) / (1 - (1 + i)^(-n)) where P=loan_amount, i=monthly_interest_rate, and n=number_of_payments over the loan’s lifetime
		/// </summary>
		/// <returns>TotalMonthlyPayment</returns>
		public double GetTotalMonthlyPayment(double loan_amount, double monthly_interest_rate, int number_of_payments)
		{
			double P = loan_amount;
			double i = (monthly_interest_rate / 100) / 12;
			int n = number_of_payments;
			double numerator = (P * i);
			double denomenator = 1 - (Math.Pow((1 + i), (-n)));
			double PaymentValue = (numerator) / (denomenator);
			return Double.Parse(String.Format("{0:.##}", PaymentValue));
		}



		//GetInterestPayment
		/// <summary>
		/// Gets the interest payment. Takes current balance, and monthly interest rate
		/// </summary>
		/// <returns>InterestPayment</returns>
		public double GetInterestPayment(double current_loan_balance, double monthly_interest_rate)
		{
			double i = (monthly_interest_rate / 100) / 12;
			double InterestPayment = current_loan_balance * i;
			return Double.Parse(String.Format("{0:.##}", InterestPayment));
		}


		//GetCurrentLoanBalance
		/// <summary>
		/// Gets the balance for the loan. Takes initial balance, and principal payment
		/// </summary>
		/// <returns>CurrentLoanBalance</returns>
		public double GetCurrentLoanBalance(double initial_balance, double principal_payment)
		{
			double CurrentLoanBalance = initial_balance - principal_payment;

			//return 0.0 if negative
			if (CurrentLoanBalance < 0)
			{
				return 0.0;
			}
			return Double.Parse(String.Format("{0:.##}", CurrentLoanBalance));
		}

		//FormatPaymentDate
		/// <summary>
		/// Formats the date for Payment Date
		/// </summary>
		/// <returns>Formated Date</returns>
		public string FormatPaymentDate(DateTime StartDate, int range)
        {
            DateTime NewDate = StartDate.AddMonths(range - 1); //parameter is one plus the number
            string FormatedDate = NewDate.ToString("MMM yyyy");
            return FormatedDate;
        }

    }
}



