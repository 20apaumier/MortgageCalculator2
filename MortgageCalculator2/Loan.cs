using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator2
{
    public class Loan
    {
        // Loan class should accept loan amount, loan length in months, and monthly interest rate in a constructor
        public readonly int ID;
        private readonly double _loanAmount;
        private readonly int _loanLengthInYears;
        private readonly double _yearlyInterestRate;

        // properties for storing:
        // current month, monthly payment, principal paid off per month,
        // interest paid per month, total interest paid, balance left on loan
        public int CurrentMonth { get; set; }
        public double MonthlyPayment { get; set; }
        public double PrincialPaidPerMonth { get; set; }
        public double InterestPaidPerMonth { get; set; }
        public double TotalInterestPaid { get; set; }
        public double BalanceLeftOnLoan { get; set; }

        public Loan(double loanAmount, int loanLengthInYears, double yearlyInterestRate)
        {
            // handle the inputs
            if (loanAmount <= 0)
            {
                throw new ArgumentException("The loan amount must be greater than 0");
            }
            else { _loanAmount = loanAmount; }

            if (loanLengthInYears <= 0)
            {
                throw new ArgumentException("The loan length in years must be greater than 0");
            }
            else { _loanLengthInYears = loanLengthInYears; }

            if (yearlyInterestRate < 0)
            {
                throw new ArgumentException("The yearly interest rate cannot be negative");
            }
            else { _yearlyInterestRate = yearlyInterestRate; }

            // set the id of the loan to the count of the loans in the bank + 1
            ID = Bank.GetAmountOfLoans() + 1;
            CurrentMonth = 0;
            TotalInterestPaid = 0;
            MonthlyPayment = CalculateMonthlyMortgagePayment(loanAmount, loanLengthInYears, yearlyInterestRate);
            BalanceLeftOnLoan = loanAmount;
        }

        // method to calculate monthly payment for mortgage
        public static double CalculateMonthlyMortgagePayment(double loanAmount, int loanLengthInYears, double yearlyInterestRate)
        {
            // do the conversions
            double monthlyInterestRate = yearlyInterestRate / 12 / 100; // convert to 0.05 format
            int totalPayments = loanLengthInYears * 12;

            // do the calculations
            double monthlyPayment = loanAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalPayments)) / (Math.Pow(1 + monthlyInterestRate, totalPayments) - 1);
            monthlyPayment = Math.Round(monthlyPayment, 2);

            return monthlyPayment;
        }

        // method to increment the month of the loan and update that data
        // so that we can show the proper data to the user
        public void IncrementMonth()
        {
            AddOneMonth();
            GetInterestPaidPerMonth();
            GetPrincipalPaidPerMonth();
            GetTotalInterestPaid();
            GetBalanceLeftOnLoan();
        }

        private void AddOneMonth() { CurrentMonth += 1; }
        private void GetInterestPaidPerMonth() { InterestPaidPerMonth = Math.Round(BalanceLeftOnLoan * (_yearlyInterestRate / 12 / 100), 2); }
        private void GetPrincipalPaidPerMonth() { PrincialPaidPerMonth = Math.Round(MonthlyPayment - InterestPaidPerMonth, 2); }
        private void GetTotalInterestPaid() { TotalInterestPaid += InterestPaidPerMonth; }
        private void GetBalanceLeftOnLoan() { BalanceLeftOnLoan = Math.Round(BalanceLeftOnLoan - PrincialPaidPerMonth, 2); }

    }
}
