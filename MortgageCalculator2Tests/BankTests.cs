using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator2.Tests
{
    [TestClass()]
    public class BankTests
    {
        [TestMethod()]
        public void GetAmountOfLoansTest()
        {
            int amountOfLoansToStart = Bank.GetAmountOfLoans();
            Loan loan = new Loan(25000.00, 5, 5);
            Loan loan2 = new Loan(600000.00, 3, 7);
            Bank.AddLoan(loan);
            Bank.AddLoan(loan2);
            Assert.AreEqual(amountOfLoansToStart + 2, Bank.GetAmountOfLoans());
        }

        [TestMethod]
        public void TestGetLoanByID()
        {
            Loan loan = new Loan(25000.00, 5, 5);
            Bank.AddLoan(loan);
            Loan loan2 = Bank.GetLoan(1);
            Assert.IsNotNull(loan2 as Loan);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetLoanByIDExceptionIfNotFound()
        {
            Loan loan2 = Bank.GetLoan(25);
        }

        [TestMethod]
        public void TestRemovingLoanFromBank()
        {
            int numOfLoansBefore = Bank.GetAmountOfLoans();
            Loan loan = Bank.GetLoan(1);
            Bank.DeleteLoan(loan);
            int numOfLoansAfter = Bank.GetAmountOfLoans();

            Assert.IsTrue(numOfLoansBefore > numOfLoansAfter);
        }

        [TestMethod]
        public void TestAddingLoanToBank()
        {
            int numOfLoansBefore = Bank.GetAmountOfLoans();
            Loan loan = new Loan(25000.00, 5, 5);
            Bank.AddLoan(loan);
            int numOfLoansAfter = Bank.GetAmountOfLoans();

            Assert.IsTrue(numOfLoansBefore < numOfLoansAfter);
        }

        [TestMethod]
        public void TestUpdatingLoanInBank()
        {
            Loan loan = new Loan(400000, 10, 5);
            Bank.AddLoan(loan);
            Loan newLoan = new Loan(450000, 10, 5);
            Bank.UpdateLoan(loan.ID, newLoan);
            Assert.AreEqual(450000, Bank.GetLoan(newLoan.ID).BalanceLeftOnLoan);
        }

        [TestMethod]
        public void TestMonthlyIncrementForLoan()
        {
            Loan loan = new Loan(25000.00, 5, 5);
            Bank.AddLoan(loan);
            Bank.IncrementMonth();
            Assert.AreEqual(1, loan.CurrentMonth);
        }

        [TestMethod]
        public void TestMonthlyIncrementDataForLoan()
        {
            Loan loan = new Loan(25000.00, 5, 5);
            Bank.AddLoan(loan);
            Bank.IncrementMonth();
            Assert.AreEqual(1, loan.CurrentMonth);
            Assert.AreEqual(471.78, loan.MonthlyPayment);
            Assert.AreEqual(367.61, loan.PrincialPaidPerMonth);
            Assert.AreEqual(104.17, loan.InterestPaidPerMonth);
            Assert.AreEqual(104.17, loan.TotalInterestPaid);
            Assert.AreEqual(24632.39, loan.BalanceLeftOnLoan);
        }

        [TestMethod]
        public void TestMultipleMonthlyIncrementsDataForALoan()
        {
            Loan loan = new Loan(25000.00, 5, 5);
            Bank.AddLoan(loan);
            Bank.IncrementMonth();
            Bank.IncrementMonth();
            Bank.IncrementMonth();
            Assert.AreEqual(3, loan.CurrentMonth);
            Assert.AreEqual(471.78, loan.MonthlyPayment);
            Assert.AreEqual(370.68, loan.PrincialPaidPerMonth);
            Assert.AreEqual(101.10, loan.InterestPaidPerMonth);
            Assert.AreEqual(307.90, loan.TotalInterestPaid);
            Assert.AreEqual(23892.56, loan.BalanceLeftOnLoan);
        }
    }
}