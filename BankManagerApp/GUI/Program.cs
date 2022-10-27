using System;
using BankManagerApp.Interfaces;
using BankManagerApp.BusinessLogic;
namespace BankManagerApp.GUI
    
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBankBranchManager bankBranchManager = new BankBranchManager();
            bool isValidChoice = false;

            do
            {
                Console.WriteLine("Welcome to Techcombank's Bank Manager App");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Create a new branch");
                Console.WriteLine("2. Add a new customer");
                Console.WriteLine("3. Add a new account");
                Console.WriteLine("4. Display customer information");
                Console.WriteLine("5. Display all transaction history");
                Console.WriteLine("6. Display all accounts with highest balance");
                Console.WriteLine("7. Display all customers by total balance");
                Console.WriteLine("8. Display the customer with the most transactions");
                Console.Write("Choice: ");
                isValidChoice = int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        bankBranchManager.AddNewBankBranch();
                        Console.WriteLine();
                        break;
                    case 2:
                        bankBranchManager.AddNewCustomer();
                        Console.WriteLine();
                        break;
                    case 3:
                        bankBranchManager.AddNewAccount();
                        Console.WriteLine();
                        break;
                    case 4:
                        bankBranchManager.DisplayCustomerDetails();
                        Console.WriteLine();
                        break;
                    case 5:
                        bankBranchManager.DisplayAllTransactionHistory();
                        Console.WriteLine();
                        break;
                    case 6:
                        bankBranchManager.ListAllAccountsWithHighestBalance();
                        Console.WriteLine();
                        break;
                    case 7:
                        bankBranchManager.DisplayCustomersByTotalBalance();
                        Console.WriteLine();
                        break;
                    case 8:
                        bankBranchManager.DisplayCustomerWithTheMostTransactions();
                        Console.WriteLine();
                        break;
                    case 9:
                        bankBranchManager.WithdrawMoney();
                        break;
                    case 10:
                        bankBranchManager.DepositMoney();
                        break;
                    default:
                        Console.WriteLine("App exited.");
                        break;
                }
            } while (isValidChoice);



        }
    }
}