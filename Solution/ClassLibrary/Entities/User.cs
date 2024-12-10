using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class User
    {
        private int userId;
        private string userName;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string phonenumber;
        private string address;
        private DateTime dateOfBirth;
        private UserRole role;

        public int UserId
        {
            get { return userId; }
            set { ; }
        }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Required(ErrorMessage = "Email is required.")]
        //[DateTimeConstant()]
        //[StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string PhoneNumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public UserRole Role
        {
            get { return role; }
            set { role = value; }
        }

        public User(int userId, string userName, string firstName, string lastName, string email, string password, DateTime dateOfBirth, UserRole role, string phonenumber, string address)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.DateOfBirth = dateOfBirth;
            this.Role = role;
            this.PhoneNumber = phonenumber;
            this.Address = address;
        }

        public User() { }
        
    }
}
