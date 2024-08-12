using MortgageCalculator;
using Spectre;
using Spectre.Console;
using System.Drawing;

namespace MortgageCalculatorConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // check if user wants interactive mode or batch mode
                if (args[0] == "--interactive" || args[0] == "-i")
                {
                    RunInteractiveMode();
                }
                else if (args[0] == "--batch" || (args[0] == "-b"))
                {
                    // Ensure you have the correct number of arguments for batch mode
                    if (args.Length == 4)
                    {
                        double loanAmount;
                        int loanLength;
                        double interestRate;

                        // Parse the arguments
                        if (double.TryParse(args[1], out loanAmount) &&
                            int.TryParse(args[2], out loanLength) &&
                            double.TryParse(args[3], out interestRate))
                        {
                            RunBatchMode(loanAmount, loanLength, interestRate);
                        }
                        else
                        {
                            Console.WriteLine("Invalid arguments for batch mode.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Batch mode requires 3 arguments: loan amount, interest rate, and duration.");
                }
            }
            else
            {
                // run standard mode
                RunStandardMode();
            }
        }

        private static void RunStandardMode()
        {
            AnsiConsole.Write(new FigletText("Mortgage Calculator").Color(Spectre.Console.Color.Green));

            // Get loan amount
            double loanAmount = AnsiConsole.Prompt(
                new TextPrompt<double>("Enter the loan amount:")
                    .PromptStyle("green")
                    .Validate(amount => amount > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Loan amount must be positive[/]"))
            );

            // Get loan length in years
            int loanLengthInYears = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter the loan length in years:")
                    .PromptStyle("green")
                    .Validate(years => years > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Loan length must be positive[/]"))
            );

            // Get yearly interest rate
            double yearlyInterestRate = AnsiConsole.Prompt(
                new TextPrompt<double>("Enter the yearly interest rate (in %):")
                    .PromptStyle("green")
                    .Validate(rate => rate > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Interest rate must be positive[/]"))
            );

            Loan loan = new Loan(loanAmount, loanLengthInYears, yearlyInterestRate);

            AnsiConsole.MarkupLine($"[bold green]Monthly Payment: {loan.MonthlyPayment}[/]");

            // Create and display the table
            var table = new Table()
               .Border(TableBorder.Rounded)
               .BorderColor(Spectre.Console.Color.SpringGreen4);

            table.AddColumn(new TableColumn("Month").Centered());
            table.AddColumn(new TableColumn("Payment Amount").RightAligned());
            table.AddColumn(new TableColumn("Principal Payment").RightAligned());
            table.AddColumn(new TableColumn("Interest Payment").RightAligned());
            table.AddColumn(new TableColumn("Total Interest").RightAligned());
            table.AddColumn(new TableColumn("Remaining Balance").RightAligned());

            int paymentsInMonths = loanLengthInYears * 12;
            int i = 0;

            while (i < paymentsInMonths)
            {
                table.AddRow(
                    loan.CurrentMonth.ToString(),
                    loan.MonthlyPayment.ToString("C"),
                    loan.PrincialPaidPerMonth.ToString("C"),
                    loan.InterestPaidPerMonth.ToString("C"),
                    loan.TotalInterestPaid.ToString("C"),
                    loan.BalanceLeftOnLoan.ToString("C")
                );
                i++;
                loan.IncrementMonth();
            }

            AnsiConsole.Write(table);

        }


        private static void RunInteractiveMode()
        {
            AnsiConsole.Write(new FigletText("Interactive Mortgage Calculator").Color(Spectre.Console.Color.Green));

            bool continueFlag = true;

            while (continueFlag)
            {
                // Get loan amount
                double loanAmount = AnsiConsole.Prompt(
                    new TextPrompt<double>("Enter the loan amount:")
                        .PromptStyle("green")
                        .Validate(amount => amount > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Loan amount must be positive[/]"))
                );

                // Get loan length in years
                int loanLengthInYears = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter the loan length in years:")
                        .PromptStyle("green")
                        .Validate(years => years > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Loan length must be positive[/]"))
                );

                // Get yearly interest rate
                double yearlyInterestRate = AnsiConsole.Prompt(
                    new TextPrompt<double>("Enter the yearly interest rate (in %):")
                        .PromptStyle("green")
                        .Validate(rate => rate > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Interest rate must be positive[/]"))
                );

                Loan loan = new Loan(loanAmount, loanLengthInYears, yearlyInterestRate);

                AnsiConsole.MarkupLine($"[bold green]Monthly Payment: {loan.MonthlyPayment}[/]");

                // Create and display the table
                var table = new Table()
                   .Border(TableBorder.Rounded)
                   .BorderColor(Spectre.Console.Color.Green);

                table.AddColumn(new TableColumn("Month").Centered());
                table.AddColumn(new TableColumn("Payment Amount").RightAligned());
                table.AddColumn(new TableColumn("Principal Payment").RightAligned());
                table.AddColumn(new TableColumn("Interest Payment").RightAligned());
                table.AddColumn(new TableColumn("Total Interest").RightAligned());
                table.AddColumn(new TableColumn("Remaining Balance").RightAligned());

                int paymentsInMonths = loanLengthInYears * 12;
                int i = 0;

                while (i < paymentsInMonths)
                {
                    table.AddRow(
                        loan.CurrentMonth.ToString(),
                        loan.MonthlyPayment.ToString("C"),
                        loan.PrincialPaidPerMonth.ToString("C"),
                        loan.InterestPaidPerMonth.ToString("C"),
                        loan.TotalInterestPaid.ToString("C"),
                        loan.BalanceLeftOnLoan.ToString("C")
                    );
                    i++;
                    loan.IncrementMonth();
                }

                AnsiConsole.Write(table);
            }

            continueFlag = AnsiConsole.Confirm("[green]Do you want to calculate another mortgage?[/]");

        }

        private static void RunBatchMode(double loanAmount, int loanLength, double interestRate)
        {
            Console.WriteLine($"Running batch mode with loanAmount: {loanAmount}, loanLength: {loanLength}, interestRate: {interestRate}");
        }
    }
}

