namespace BankManagerApp.BusinessObjects

{
    public class Customer
    {
        public string ID { get; set; }//CUSXXXX
        public List<Account> Accounts = new List<Account>();
        public decimal TotalBalance { get; set; } = 0;
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

        public void AddNewAccount()
        {
            //Input fields

            string accountID = "ACC" + Utils.Utils.GenerateRandomNumString();

            Console.WriteLine("Enter your initial account balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                if (balance>0)
                {
                    Accounts.Add(new Account(accountID, balance));
                    Console.WriteLine("Account added successfully");
                }
                else
                {
                    Console.WriteLine("You can't ");
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
    }
}
