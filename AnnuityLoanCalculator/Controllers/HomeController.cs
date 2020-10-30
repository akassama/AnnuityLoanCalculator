using AnnuityLoanCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnuityLoanCalculator.Controllers
{
    public class HomeController : Controller
    {
        AppFunctions functions = new AppFunctions();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoanModel loanModel)
        {
            if (ModelState.IsValid)
            {
                //set tempdata for processing report
                TempData["LoanAmount"] = loanModel.LoanSum;
                TempData["LoanTerm"] = loanModel.LoanTerm;
                TempData["LoanInterest"] = loanModel.LoanInterest;
                TempData["PaymentStep"] = loanModel.PaymentStep;

                return RedirectToAction("Results");
            }
            return View(loanModel);
        }


        public ActionResult Results()
        {
            //if any variable is null
            if (TempData["LoanAmount"] == null || TempData["LoanTerm"] == null || TempData["LoanInterest"] == null)
            {
                TempData["ErrorMessage"] = "Missing input data.";

                return RedirectToAction("Index");
            }

            double LoanAmount = Double.Parse(TempData["LoanAmount"].ToString());
            double LoanInterest = Double.Parse(TempData["LoanInterest"].ToString());
            int NumberOfPayments = Int32.Parse(TempData["LoanTerm"].ToString());
            double MonthlyPayment = functions.GetTotalMonthlyPayment(LoanAmount, LoanInterest, NumberOfPayments);

            List<ResultsModel> IList = new List<ResultsModel>()
            {

            };

            ResultsModel resultData = new ResultsModel();
            resultData.PaymentNumber = MonthlyPayment;
            resultData.PaymentDate = functions.FormatPaymentDate(DateTime.Now, 1);
            resultData.PrincipalPayment = functions.GetPrincipalPayment(MonthlyPayment, LoanAmount, LoanInterest);
            resultData.InterestPayment = functions.GetInterestPayment(functions.GetPrincipalPayment(MonthlyPayment, LoanAmount, LoanInterest), LoanInterest);
            resultData.PrincipalBalance = LoanAmount;

            for (int i = 1; i <= NumberOfPayments; i++)
            {
                //get values

                double PrincipalPayment = functions.GetPrincipalPayment(MonthlyPayment, resultData.PrincipalBalance, LoanInterest);
                double CurrentLoanBalance = functions.GetCurrentLoanBalance(resultData.PrincipalBalance, functions.GetPrincipalPayment(MonthlyPayment, resultData.PrincipalBalance, LoanInterest));
                double InterestPayment = functions.GetInterestPayment(resultData.PrincipalBalance, LoanInterest);


                //add values to list
                IList.Add(
                        new ResultsModel
                        {
                            PaymentNumber = MonthlyPayment,
                            PaymentDate = functions.FormatPaymentDate(DateTime.Now, i),
                            PrincipalPayment = PrincipalPayment,
                            InterestPayment = InterestPayment,
                            PrincipalBalance = CurrentLoanBalance,
                        }
                    );

                //set new values
                resultData.PrincipalBalance = CurrentLoanBalance;
            }

            return View(IList);
        }


        public ActionResult AnnuityCalculator()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnnuityCalculator(LoanModel loanModel)
        {
            if (ModelState.IsValid)
            {
                //set tempdata for processing report
                TempData["LoanAmount"] = loanModel.LoanSum;
                TempData["LoanTerm"] = loanModel.LoanTerm;
                TempData["LoanInterest"] = loanModel.LoanInterest;
                TempData["PaymentStep"] = loanModel.PaymentStep;

                return RedirectToAction("Results2");
            }
            return View(loanModel);
        }


        public ActionResult Results2()
        {
            //if any of the variable is null
            if (TempData["LoanAmount"] == null || TempData["LoanTerm"] == null || TempData["LoanInterest"] == null || TempData["PaymentStep"] == null)
            {
                TempData["ErrorMessage"] = "Missing input data.";

                return RedirectToAction("AnnuityCalculator");
            }

            double LoanAmount = Double.Parse(TempData["LoanAmount"].ToString());
            double LoanInterest = Double.Parse(TempData["LoanInterest"].ToString());
            int LoanTerm = Int32.Parse(TempData["LoanTerm"].ToString());
            int PaymentStep = Int32.Parse(TempData["PaymentStep"].ToString());

            //formula according to requirements (email)
            double P = LoanAmount;
            double interest = LoanInterest * LoanTerm;
            int n = LoanTerm / PaymentStep;
            double MonthlyPayment = functions.GetTotalMonthlyPayment(P, interest, n);

            List<ResultsModel> IList = new List<ResultsModel>()
            {

            };

            ResultsModel resultData = new ResultsModel();
            resultData.PaymentNumber = MonthlyPayment;
            resultData.PaymentDate = null;
            resultData.PrincipalPayment = functions.GetPrincipalPayment(MonthlyPayment, LoanAmount, LoanInterest);
            resultData.InterestPayment = functions.GetInterestPayment(functions.GetPrincipalPayment(MonthlyPayment, LoanAmount, LoanInterest), LoanInterest);
            resultData.PrincipalBalance = LoanAmount;

            for (int i = 1; i <= LoanTerm; i += PaymentStep)
            {
                //get values
                double PrincipalPayment = functions.GetPrincipalPayment(MonthlyPayment, resultData.PrincipalBalance, LoanInterest);
                double CurrentLoanBalance = functions.GetCurrentLoanBalance(resultData.PrincipalBalance, functions.GetPrincipalPayment(MonthlyPayment, resultData.PrincipalBalance, LoanInterest));
                double InterestPayment = functions.GetInterestPayment(resultData.PrincipalBalance, LoanInterest);

                //add values to list
                IList.Add(
                        new ResultsModel
                        {
                            PaymentNumber = MonthlyPayment,
                            PaymentDate = "Payment on Day " + i,
                            PrincipalPayment = PrincipalPayment,
                            InterestPayment = InterestPayment,
                            PrincipalBalance = CurrentLoanBalance,
                        }
                    );

                //set new values
                resultData.PrincipalBalance = CurrentLoanBalance;
            }

            return View(IList);
        }
    }
}