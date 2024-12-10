using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogic.Entities;
using BusinessLogic.Managers;

namespace BusinessLogic.Strategies
{
    public class StaffMemberStrategy : IAuthenticationStrategy
    {
        public UserManager userManager { get; set; }
        public EncriptionManager encriptionManager { get; set; }

        public StaffMemberStrategy(UserManager userManager, EncriptionManager encriptionManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.encriptionManager = encriptionManager ?? throw new ArgumentNullException(nameof(encriptionManager));
        }

        public User? Login(string email, string password)
        {
            var user = userManager.GetUserByEmail(email);

            if (user == null || user.Role != UserRole.StaffMember)
            {
                return null;
            }

            bool isValid = encriptionManager.CheckCredentials(user.Password, password);
            return isValid ? user : null;
        }


        public void Register(User user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.Role != UserRole.StaffMember)
                throw new InvalidOperationException("This login is only for staff members.");

            user.Password = encriptionManager.HashPassword(password);

            userManager.AddUser(user);
        }
    }
}
