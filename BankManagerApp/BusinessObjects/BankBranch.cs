using System.Text.RegularExpressions;

namespace BankManagerApp.BusinessObjects
{
    public class BankBranch
    {
        public string ID { get; private set; }//TECHXXX
        public string Name { get; set; } = "Unknown";
        public (string hamlet, string ward, string district, string city) Address { get; set; }
        public List<Customer> CustomerList { get; set; }

        public BankBranch(string iD, string name, (string hamlet, string ward, string district, string city) address)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address;
        }

        public void AddNewCustomer()
        {
            //Input the fields
            string id = Console.ReadLine();
            Regex custIDRegex = new Regex(Utils.Utils.CustomerIDFormat);
            if (!custIDRegex.IsMatch(id))
            {
                Console.WriteLine("Wrong ID Format (CUSXXX where X is a digit from 0-9)");
                return;
            }
            string name = Console.ReadLine();
            string hamlet = Console.ReadLine();
            string ward = Console.ReadLine();
            string district = Console.ReadLine();
            string city = Console.ReadLine();
            (string hamlet, string ward, string district, string city) address = (hamlet, ward, district, city);

            CustomerList.Add(new Customer(id, name, address));
        }

        public void DisplayEveryTransactions()
        {
            //For each customer in the list, get all the transaction history
            foreach (var transaction in GetAllTransactions())
            {
                Console.WriteLine(transaction);
            }
        }

        private List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactionList = new List<Transaction>();

            //1. Get a customer in the CustomerList
            //2. Get all transactions of that customer
            //3. Move to the next customer in the list
            //4. Repeat 
            foreach (var customer in CustomerList)
            {
                foreach (var account in customer.Accounts)
                {
                    foreach (var transaction in account.TransactionHistory)
                    {
                        transactionList.Add(transaction);
                    }
                }
            }

            return transactionList;
        }

        public Customer GetCustomerByName(string name)
        {
            foreach (var customer in CustomerList)
            {
                if (customer.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerByID(string id)
        {
            foreach (var customer in CustomerList)
            {
                if (customer.ID.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return customer;
                }
            }
            return null;
        }

    }
}
