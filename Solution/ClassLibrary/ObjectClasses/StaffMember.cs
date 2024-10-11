using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ObjectClasses
{
    public class StaffMember : User
    {
        [Required(ErrorMessage = "Position is required.")]
        [StringLength(50, ErrorMessage = "Position cannot be longer than 50 characters.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        public StaffMember(int userId, string firstName, string lastName, string username, string email, string password, string position, decimal salary)
            : base(userId, firstName, lastName, username, email, password)
        {
            this.Position = position;
            this.Salary = salary;
        }

        public StaffMember(string firstName, string lastName, string username, string email, string password, string position, decimal salary)
            : base(firstName, lastName, username, email, password)
        {
            this.Position = position;
            this.Salary = salary;
        }

        public override void Login()
        {
            
        }

    }
}
