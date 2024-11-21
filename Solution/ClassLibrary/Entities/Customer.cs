using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Customer : User
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(50, ErrorMessage = "Phone number cannot be longer than 50 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        // Constructor with all required fields
        public Customer(int userId, string firstName, string lastName, string username, string email, string password, string phoneNumber, string address)
            : base(userId, firstName, lastName, username, email, password)
        {
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public Customer(string firstName, string lastName, string username, string email, string password, string phoneNumber, string address)
            : base(firstName, lastName, username, email, password)
        {
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public Customer()
        {
        }
        public override void Login()
        {
        }

    }
}
