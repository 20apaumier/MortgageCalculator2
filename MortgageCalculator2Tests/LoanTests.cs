using MortgageCalculator2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator2.Tests
{
    [TestClass()]
    public class LoanTests
    {
        [TestMethod]
        public void TestCallingCalculateMonthlyMortgagePayment()
        {
            Assert.IsNotNull(Loan.CalculateMonthlyMortgagePayment(1.00, 12, 1.00));
        }

        [TestMethod]
        public void TestCalculateMonthlyMortgagePayment()
        {
            Assert.AreEqual(471.78, Loan.CalculateMonthlyMortgagePayment(25000, 5, 5));
            Assert.AreEqual(463.12, Loan.CalculateMonthlyMortgagePayment(100000, 30, 3.75));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGivenNegativeLoanAmount()
        {
            Loan loan = new Loan(-120000.00, 12, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGivenNegativeLoanLengthInMonths()
        {
            Loan loan = new Loan(120000.00, -12, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGivenNegativeMonthlyInterestRate()
        {
            Loan loan = new Loan(120000.00, 12, -5);
        }
    }
}