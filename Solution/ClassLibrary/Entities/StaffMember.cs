using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class StaffMember : User
    {
        [Required(ErrorMessage = "Position is required.")]
        [StringLength(50, ErrorMessage = "Position cannot be longer than 50 characters.")]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public string Role { get; set; }

        public StaffMember(int userId, string firstName, string lastName, string username, string email, string password, int staffId, string role)
            : base(userId, firstName, lastName, username, email, password)
        {
            StaffId = staffId;
            Role = role;
        }

        public StaffMember(string firstName, string lastName, string username, string email, string password, int staffId, string role)
            : base(firstName, lastName, username, email, password)
        {
            StaffId = staffId;
            Role = role;
        }

        public StaffMember() { }
        public override void Login()
        {

        }

    }
}
