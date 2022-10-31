using BankManagerApp.BusinessObjects;
using BankManagerApp.Interfaces;
using System.Text.RegularExpressions;

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
            string customerName = Console.ReadLine()??"";
            if (customerName=="")
            {
                Console.WriteLine("Customer name can't be blank.");
                return;
            }
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
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
            string bankBranchName = Console.ReadLine()??"";
            if (bankBranchName == "")
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
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);

            string continueAddingCustomer;
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
            string bankBranchName = Console.ReadLine()??"";
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null)
            {
                Console.WriteLine("This bank branch doesn't exsit.");
                return;
            }
            //select customer
            Console.WriteLine("Which customer would you like to choose? ");
            string customerName = Console.ReadLine()??"";
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't exist.");
                return;
            }
            //add new account
            string continueAddingAccounts;
            do
            {
                customer.AddNewAccount();
                customer.UpdateTotalBalance();
                
                Console.WriteLine("Do you want to add another account? (Y/N)");
                continueAddingAccounts = Console.ReadLine() ?? "n";
            } while (continueAddingAccounts.Equals("y", StringComparison.OrdinalIgnoreCase));
        }

        public void WithdrawOrDeposit()
        {
            string input= Console.ReadLine()??"";
            if (input=="")
            {
                Console.WriteLine("Blank spaces not allowed.");
                return;
            }
            if (input.Equals("d",StringComparison.OrdinalIgnoreCase))
            {
                DepositMoney();
            }
            else if (input.Equals("w",StringComparison.OrdinalIgnoreCase))
            {
                WithdrawMoney();
            }
            else
            {
                Console.WriteLine("You can only input \"d\" or \"w\"");
                return;
            }
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
            string bankBranchID = "TECH" + Utils.Utils.GenerateRandomNumString();
            while (GetBankBranchByID(bankBranchID)!=null)
            {
                bankBranchID = "TECH" + Utils.Utils.GenerateRandomNumString();
            }

            Console.WriteLine("Please input the name for this bank branch: ");
            string bankBranchName = Console.ReadLine() ?? "Not chosen";
            bool bankBranchExist = doesBankBranchNameExist(bankBranchName);

            if (bankBranchExist == true)
            {
                Console.WriteLine("This bank branch already exists.");
                Console.WriteLine();
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

            BankBranches.Add(new BankBranch(bankBranchID, bankBranchName, address));
            Console.WriteLine("Bank added successfully");
            Console.WriteLine();
        }

        public void DisplayAllBankBranches()
        {
            foreach (var bankBranch in BankBranches)
            {
                Console.WriteLine($"Branch ID:{bankBranch.ID}\nBranch Name:{bankBranch.Name}\nAddress: {bankBranch.Address.hamlet} hamlet, ward {bankBranch.Address.ward}, {bankBranch.Address.district} District, {bankBranch.Address.city} City");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        

        public void DisplayAllTransactionHistory()
        {
            Console.WriteLine("Bank branch name: ");
            string bankBranchName = Console.ReadLine() ?? "";
            if (bankBranchName == "")
            {
                Console.WriteLine("Bank name can't be null.");
                return;
            }
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch==null)
            {
                Console.WriteLine("This bank doesn't exist.");
                return;
            }
            bankBranch.DisplayEveryTransactions();
            Console.WriteLine();
        }

        public void ListAllAccountsWithHighestBalance()
        {
            Console.WriteLine("Which bank branch?");
            string bankBranchName = Console.ReadLine()??"";
            bool bankExisted = doesBankBranchNameExist(bankBranchName);
            if (!bankExisted)
            {
                Console.WriteLine("This bank doesn't exist.");
                return;
            }
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            foreach (var account in bankBranch.GetAllAccountsWithHighestBalance())
            {
                if (account == null)
                {
                    Console.Write("");
                }
                else
                {
                    Console.WriteLine($"Account number : {account.AccountNumber}");
                }
            }
            Console.WriteLine();
        }

        public void DisplayCustomersByTotalBalance()
        {
            List<Customer> customers = GetAllCustomers().OrderByDescending(cus=>cus.TotalBalance).ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID:{customer.ID}\nName:{customer.Name}\nAddress:{customer.Address.hamlet}, {customer.Address.ward}, {customer.Address.district}, {customer.Address.city}\nTotal balance: {customer.TotalBalance}");
            }
            Console.WriteLine();
        }

        public void DisplayCustomerWithTheMostTransactions()
        {
            Customer customer = GetAllCustomers().MaxBy(x => x.TransactionCount);
            if (customer==null)
            {
                Console.WriteLine("There is no customer available");
                return;
            }
            Console.WriteLine("This is the customer with the highest transaction count");
            Console.WriteLine($"Customer ID:{customer.ID}\nName:{customer.Name}\nAddress:{customer.Address.hamlet}, {customer.Address.ward}, {customer.Address.district}, {customer.Address.city}");
            Console.WriteLine();
        }

        private List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();
            foreach (var bankBranch in BankBranches)
            {
                foreach (var customer in bankBranch.CustomerList)
                {
                    allCustomers.Add(customer);
                }
            }
            return allCustomers;
        }

        public void DepositMoney()
        {
            //Get the bank branch name
            Console.WriteLine("Please enter the bank branch's name: ");
            string bankBranchName = Console.ReadLine()??"";
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null)
            {
                Console.WriteLine("This bank branch doesn't exist.");
                Console.WriteLine();
                return;
            }

            //Get the customer
            Console.WriteLine("Please enter the customer's name: ");
            string customerName = Console.ReadLine()??"";
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't exist");
                Console.WriteLine();
                return;
            }

            //Get the account
            Console.WriteLine("Please enter the account number: ");
            string accountNumber = Console.ReadLine() ?? "";
            Regex accountNumberRegex = new Regex(Utils.Utils.AccountNumberFormat);
            if (!accountNumberRegex.IsMatch(accountNumber))
            {
                Console.WriteLine("Wrong account format (ACCXXX where X is a digit from 0-9)");
                return;
            }
            Account account = customer.GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("There's no such account.");
                Console.WriteLine();
                return;
            }
            account.Deposit();
            customer.UpdateTotalBalance();
            customer.TransactionCount++;
            Console.WriteLine("Deposit successfully");
            Console.WriteLine();
        }

        public void WithdrawMoney()
        {
            //Get the bank branch name
            Console.WriteLine("Please enter the bank branch's name: ");
            string bankBranchName = Console.ReadLine()??"";
            BankBranch bankBranch = GetBankBranchByName(bankBranchName);
            if (bankBranch == null)
            {
                Console.WriteLine("This bank branch doesn't exist.");
                Console.WriteLine();
                return;
            }

            //Get the customer
            Console.WriteLine("Please enter the customer's name: ");
            string customerName = Console.ReadLine()??"";
            Customer customer = bankBranch.GetCustomerByName(customerName);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't exist");
                Console.WriteLine();
                return;
            }

            //Get the account
            Console.WriteLine("Please enter the account number: ");
            string accountNumber = Console.ReadLine()??"";
            Regex accountNumberRegex = new Regex(Utils.Utils.AccountNumberFormat);
            if (!accountNumberRegex.IsMatch(accountNumber))
            {
                Console.WriteLine("Wrong account number format (ACCXXX where X is a digit from 0-9)");
                Console.WriteLine();
                return;
            }
            Account account = customer.GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("There's no such account.");
                Console.WriteLine();
                return;
            }

            account.Withdraw();
            customer.UpdateTotalBalance();
            customer.TransactionCount++;
            Console.WriteLine("Withdraw successfully");
        }
    }
}
