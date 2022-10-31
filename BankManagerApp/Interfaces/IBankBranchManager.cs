namespace BankManagerApp.Interfaces
{
    public interface IBankBranchManager
    {
        void AddNewAccount();
        void AddNewBankBranch();
        void AddNewCustomer();
        
        void DisplayAllBankBranches();
        void DisplayAllTransactionHistory();
        void DisplayCustomerDetails();
        void DisplayCustomersByTotalBalance();
        void DisplayCustomerWithTheMostTransactions();
        void ListAllAccountsWithHighestBalance();
        void WithdrawOrDeposit();
    }
}