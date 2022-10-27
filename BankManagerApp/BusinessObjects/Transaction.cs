namespace BankManagerApp.BusinessObjects
{
    public class Transaction

    {
        public string TransactionNumber { get; private set; }//TRANSXXX
        public string TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public bool WithdrawOrDeposit { get; set; } //True == Withdraw, False == Deposit

        public Transaction(string transactionNumber, decimal transactionAmount, bool withdrawOrDeposit)
        {
            TransactionNumber= transactionNumber;
            TransactionDate = DateTime.Now.ToShortDateString();
            TransactionAmount = transactionAmount;
            WithdrawOrDeposit = withdrawOrDeposit;
        }
    }
}
