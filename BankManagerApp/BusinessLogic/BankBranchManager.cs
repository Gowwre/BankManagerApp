using System.Text.RegularExpressions;
using BankManagerApp.BusinessObjects;
using BankManagerApp.Interfaces;
using BankManagerApp.Utils;

namespace BankManagerApp.BusinessLogic
{
    public class BankBranchManager : IBankBranchManager
    {
        public static List<BankBranch> BankBranches = new List<BankBranch>();


        public void DisplayCustomerDetails()
        {
            //Prompt for bank branch
            Console.Write("Please enter the bank branch name: ");
            string bankBranchName = Console.ReadLine() ?? "";
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null || bankBranchName == "")
            {
                Console.WriteLine("Please enter the correct branch name.");
                return;
            }
            //Prompt for customer name
            Console.Write("Please enter the customer name: ");
            string customerName = Console.ReadLine();
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null || customerName == "")
            {
                Console.WriteLine("This person does not exist.");
                return;
            }
            Console.WriteLine();

            //Prints out customer details
            Console.WriteLine($"Customer ID:{customer.ID}\nName:{customer.Name}\nAddress:{customer.Address.hamlet}, {customer.Address.ward}, {customer.Address.district}, {customer.Address.city}");
            Console.WriteLine("Accounts in possession: ");
            foreach (var account in customer.Accounts)
            {
                Console.WriteLine($"{account.AccountNumber}");
            }
        }

        public void AddNewCustomer()
        {
            //Get the bank branch
            Console.WriteLine("Enter bank branch name: ");
            string bankBranchName = Console.ReadLine();
            if (bankBranchName == null)
            {
                Console.WriteLine("You can't leave this field empty.");
                return;
            }
            bool bankBranchExists = doesBankBranchNameExist(bankBranchName);
            if (bankBranchExists == false)
            {
                Console.WriteLine("This bank doesn't exist.");
                return;
            }
            BankBranch bankBranch = GetBankBranchByName("sample");

            string continueAddingCustomer = "n";
            do
            {
                bankBranch.AddNewCustomer();
                Console.WriteLine("Customer added successfully.");
                Console.WriteLine("Do you want to add another customer? (Y/N): ");
                continueAddingCustomer = Console.ReadLine() ?? "n";
            } while (continueAddingCustomer.Equals("y", StringComparison.OrdinalIgnoreCase));
        }

        public void AddNewAccount()
        {
            //select bank branch
            Console.WriteLine("Which bank branch would you like to choose? ");
            string bankBranchName = Console.ReadLine();
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null)
            {
                Console.WriteLine("This bank branch doesn't exsit.");
                return;
            }
            //select customer
            Console.WriteLine("Which customer would you like to choose? ");
            string customerName = Console.ReadLine();
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't exist.");
                return;
            }
            //add new account
            string continueAddingAccounts = "n";
            do
            {
                customer.AddNewAccount();
                Console.WriteLine("Account added successfully!");
                Console.WriteLine("Do you want to add another account? (Y/N)");
                continueAddingAccounts = Console.ReadLine() ?? "n";
            } while (continueAddingAccounts.Equals("y", StringComparison.OrdinalIgnoreCase));
        }

        private BankBranch GetBankBranchByName(string bankBranchName)
        {


            foreach (var bankBranch in BankBranches)
            {
                if (bankBranch.Name.Equals(bankBranchName, StringComparison.OrdinalIgnoreCase))
                {
                    return bankBranch;
                }
            }
            return null;
        }

        private bool doesBankBranchNameExist(string bankBranchName)
        {
            return GetBankBranchByName(bankBranchName) != null;
        }

        private bool doesBankBranchIDExist(string bankBranchID)
        {
            return GetBankBranchByID(bankBranchID) != null;
        }

        private BankBranch GetBankBranchByID(string bankBranchID)
        {
            foreach (var bankBranch in BankBranches)
            {
                if (bankBranch.ID.Equals(bankBranchID, StringComparison.OrdinalIgnoreCase))
                {
                    return bankBranch;
                }
            }
            return null;
        }

        public void AddNewBankBranch()
        {
            //Get the fields
            Console.WriteLine("Please input the ID for this bank branch: ");
            string ID = Console.ReadLine();
            Regex bankBranchIDRegex = new Regex(Utils.Utils.BankBranchIDFormat);

            if (ID == null)
            {
                Console.WriteLine("Null ID is prohibited.");
                return;
            }

            else if (!bankBranchIDRegex.IsMatch(ID))
            {
                Console.WriteLine("Wrong ID Format (TECHXXX where X is a digit from 0-9)");
                return;
            }

            Console.WriteLine("Please input the name for this bank branch: ");
            string bankBranchName = Console.ReadLine() ?? "Not chosen";
            bool bankBranchExist = doesBankBranchNameExist(bankBranchName);

            if (bankBranchExist == true)
            {
                Console.WriteLine("This bank branch already exists.");
                return;
            }
            Console.WriteLine("Please input the address for this bank branch: ");
            Console.Write("Hamlet: ");
            string hamlet = Console.ReadLine() ?? "Not chosen";
            Console.Write("Ward: ");
            string ward = Console.ReadLine() ?? "Not chosen";
            Console.Write("District: ");
            string district = Console.ReadLine() ?? "Not chosen";
            Console.Write("City: ");
            string city = Console.ReadLine() ?? "Not chosen";
            (string hamlet, string ward, string district, string city) address = (hamlet, ward, district, city);

            BankBranches.Add(new BankBranch(ID, bankBranchName, address));
            Console.WriteLine("Bank added successfully");
        }

        public void DisplayAllBankBranches()
        {
            foreach (var bankBranch in BankBranches)
            {
                Console.WriteLine($"Branch ID:{bankBranch.ID}\nBranch Name:{bankBranch.Name}\nAddress: {bankBranch.Address.hamlet} hamlet, ward {bankBranch.Address.ward}, {bankBranch.Address.district} District, {bankBranch.Address.city} City");
                Console.WriteLine();
            }
        }

        private BankBranch GetLatestBankBranch()
        {
            return BankBranches.Last();
        }

        public void DisplayAllTransactionHistory()
        {
            Console.WriteLine("Bank branch name: ");
            string bankBranchName = Console.ReadLine();
            if (bankBranchName == null)
            {
                Console.WriteLine("Bank name can't be null.");
                return;
            }
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            bankBranch.DisplayEveryTransactions();

        }

        public void ListAllAccountsWithHighestBalance()
        {
            Console.WriteLine("Which bank branch?");
            string bankBranchName = Console.ReadLine();
            bool bankExisted = doesBankBranchNameExist(bankBranchName);
            if (!bankExisted)
            {
                Console.WriteLine("This bank doesn't exist.");
                return;
            }
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            //IEnumerable<Account> accountsWithHighestBalance  
        }

        public void DisplayCustomersByTotalBalance()
        {
            throw new NotImplementedException();
        }

        public void DisplayCustomerWithTheMostTransactions()
        {
            throw new NotImplementedException();
        }

        public void DepositMoney()
        {
            //Get the bank branch name
            Console.WriteLine("Please enter the bank branch's name: ");
            string bankBranchName = Console.ReadLine();
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch==null)
            {
                Console.WriteLine("This bank branch doesn't exist.");
                return;
            }

            //Get the customer
            Console.WriteLine("Please enter the customer's name: ");
            string customerName = Console.ReadLine();
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer==null)
            {
                Console.WriteLine("This customer doesn't exist");
                return;
            }

            //Get the account
            Console.WriteLine("Please enter the account number: ");
            string accountNumber = Console.ReadLine();
            Regex accountNumberRegex = new Regex(Utils.Utils.AccountNumberFormat);
            Account account = customer.GetAccount(accountNumber);

            account.Deposit();
        }

        public void WithdrawMoney()
        {
            //Get the bank branch name
            Console.WriteLine("Please enter the bank branch's name: ");
            string bankBranchName = Console.ReadLine();
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null)
            {
                Console.WriteLine("This bank branch doesn't exist.");
                return;
            }

            //Get the customer
            Console.WriteLine("Please enter the customer's name: ");
            string customerName = Console.ReadLine();
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't exist");
                return;
            }

            //Get the account
            Console.WriteLine("Please enter the account number: ");
            string accountNumber = Console.ReadLine();
            Regex accountNumberRegex = new Regex(Utils.Utils.AccountNumberFormat);
            Account account = customer.GetAccount(accountNumber);

            account.Withdraw();
        }
    }
}
