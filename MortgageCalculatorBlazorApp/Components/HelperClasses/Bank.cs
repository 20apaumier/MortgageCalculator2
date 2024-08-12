namespace MortgageCalculatorBlazorApp.Components.HelperClasses
{
    public static class Bank
    {
        // purpose of the bank is to store loans
        private readonly static List<Loan> _loans = new List<Loan>();

        // GetAmountOfLoans method
        public static int GetAmountOfLoans()
        {
            return _loans.Count;
        }

        // Get all loans
        public static List<Loan> GetLoans()
        {
            return _loans;
        }

        // Get Loan By ID
        public static Loan GetLoan(int loanID)
        {
            foreach (Loan loan in _loans)
            {
                if (loan.ID == loanID)
                {
                    return loan;
                }
            }
            throw new Exception("Loan not found in bank");
        }

        // Add Loan
        public static void AddLoan(Loan loan)
        {
            _loans.Add(loan);
        }

        // Update Loan
        public static void UpdateLoan(int loanIDToBeUpdated, Loan newLoanInfo)
        {
            Loan oldLoan = GetLoan(loanIDToBeUpdated);
            DeleteLoan(oldLoan);
            AddLoan(newLoanInfo);
        }

        // Delete Loan
        public static void DeleteLoan(Loan loan)
        {
            _loans.Remove(loan);
        }

        // Increment a month at the bank
        public static void IncrementMonth()
        {
            foreach (Loan loan in _loans)
            {
                loan.IncrementMonth();
            }
        }
    }
}
