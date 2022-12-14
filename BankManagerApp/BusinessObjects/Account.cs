namespace BankManagerApp.BusinessObjects
{
    public class Account : IComparable<Account>
    {
        public string AccountNumber { get; set; }//ACCXXX
        public decimal AccountBalance { get; set; }
        public List<Transaction> TransactionHistory = new List<Transaction>();

        public Account(string accountNumber, decimal accountBalance)
        {
            AccountNumber = accountNumber;
            AccountBalance = accountBalance;
        }

        public void AddNewTransaction(string transactionNumber, decimal amount, bool withdrawOrDeposit)
        {
            TransactionHistory.Add(new Transaction(transactionNumber, amount, withdrawOrDeposit));
        }

        public void Deposit()
        {
            Console.WriteLine("Enter the amount you want to deposit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Please enter numbers only.");
                return;
            }
            if (amount <= 0)
            {
                Console.WriteLine("Amount of deposit must be positive");
                return;
            }

            AccountBalance += amount;

            bool withdrawOrDeposit = false;



            string transactionNumber = "TRANS" + Utils.Utils.GenerateRandomNumString();
            while (GetTransaction(transactionNumber) != null)
            {
                transactionNumber = "TRANS" + Utils.Utils.GenerateRandomNumString();
            }
            AddNewTransaction(transactionNumber, amount, withdrawOrDeposit);
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter the amount you want to withdraw: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Please enter numbers only.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Amount of withdrawal must be positive.");
                return;
            }

            if (AccountBalance - amount < 0)
            {
                Console.WriteLine("Not sufficent funds for this withdrawal");
                return;
            }

            AccountBalance -= amount;

            bool withdrawOrDeposit = true;
            string transactionNumber = "TRANS" + Utils.Utils.GenerateRandomNumString();
            while (GetTransaction(transactionNumber) != null)
            {
                transactionNumber = "TRANS" + Utils.Utils.GenerateRandomNumString();
            }
            AddNewTransaction(transactionNumber, amount, withdrawOrDeposit);
        }

        public Transaction GetTransaction(string transactionNumber)
        {
            foreach (var transaction in TransactionHistory)
            {
                if (transaction.TransactionNumber == transactionNumber)
                {
                    return transaction;
                }
            }
            return null;
        }

        public int CompareTo(Account? otherAccount)
        {
            if (otherAccount != null)
            {
                return AccountBalance.CompareTo(otherAccount.AccountBalance);
            }
            return 0;
        }
    }
}
