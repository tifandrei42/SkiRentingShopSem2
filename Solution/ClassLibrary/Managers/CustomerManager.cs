using ClassLibrary.DataAccess;
using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Managers
{
    public class CustomerManager
    {
        private readonly CustomerMediator _customerMediator;

        public CustomerManager()
        {
            _customerMediator = new CustomerMediator();  // Initialize the mediator for database interactions
        }

        public void AddCustomer(Customer customer)
        {
            _customerMediator.CreateCustomer(customer);  
        }

        public Customer CheckCredentials(string email, string password)
        {
            var customer = _customerMediator.GetCustomerByEmail(email);

            if (customer != null && customer.Password == password)
            {
                return customer;  
            }

            return null; 
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }

        public void RegisterCustomer(Customer customer)
        {
            // Hash the password before storing
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

            _customerMediator.CreateCustomer(customer);
        }
    }
}
