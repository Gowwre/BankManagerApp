namespace BankManagerApp.BusinessObjects

{
    public class Customer
    {
        public string ID { get; set; }
        public List<Account> Accounts = new List<Account>();
        //private decimal _totalBalance;
        public decimal TotalBalance
        {
            get; set;
        }
        public string Name { get; set; } = "Blank";
        public (string hamlet, string ward, string district, string city) Address { get; set; }
        public int TransactionCount { get; set; } = 0;

        public List<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public Customer(string iD, string name, (string hamlet, string ward, string district, string city) address)
        {
            ID = iD;
            Name = name;
            Address = address;
            TotalBalance = GetTotalBalance();
        }

        public decimal GetTotalBalance()
        {
            decimal totalBalance = 0;
            foreach (var account in Accounts)
            {
                totalBalance += account.AccountBalance;
            }

            return totalBalance;
        }

        /// <summary>
        /// When a new account is added, or a transaction happens, this method will be called
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateTotalBalance()
        {
            TotalBalance = 0;
            //If a new account is added
            foreach (var acccount in Accounts)
            {
                TotalBalance += acccount.AccountBalance;
            }
        }

        public Account GetLatestAccount()
        {
            return Accounts.Last();
        }

        public void AddNewAccount()
        {
            //Input fields

            string accountNumber = "ACC" + Utils.Utils.GenerateRandomNumString();
            while (GetAccount(accountNumber) != null)
            {
                accountNumber = "ACC" + Utils.Utils.GenerateRandomNumString();
            }

            Console.WriteLine("Enter your initial account balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                if (balance > 0)
                {
                    Accounts.Add(new Account(accountNumber, balance));
                    Console.WriteLine("Account added successfully");
                }
                else
                {
                    Console.WriteLine("You can't have a negative balance");
                }
            }
            else
            {
                Console.WriteLine("You can only input decimal numbers");
                return;
            }

        }

        public Account GetAccount(string accountNumber)
        {
            foreach (var account in Accounts)
            {
                if (accountNumber.Equals(account.AccountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return account;
                }
            }
            return null;
        }


        /// <summary>
        /// Get the account with the highest balance
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Throws Exception when there is no account in possession</exception>
        public Account GetAccountWithHighestBalance()
        {
            
            var account = Accounts.MaxBy(x => x.AccountBalance);
            return account;
        }
    }
}
