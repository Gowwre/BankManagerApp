namespace BankManagerApp.Interfaces
{
    public interface IBankBranchManager
    {
        void AddNewAccount();
        void AddNewBankBranch();
        void AddNewCustomer();
        void DepositMoney();
        void DisplayAllBankBranches();
        void DisplayAllTransactionHistory();
        void DisplayCustomerDetails();
        void DisplayCustomersByTotalBalance();
        void DisplayCustomerWithTheMostTransactions();
        void ListAllAccountsWithHighestBalance();
        void WithdrawMoney();
    }
}