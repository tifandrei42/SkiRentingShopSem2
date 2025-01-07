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
    public class LoginService
    {
        public UserManager userManager { get; set; }
        public EncriptionManager encriptionManager { get; set; }

        public LoginService(UserManager userManager, EncriptionManager encriptionManager)
        {
            this.userManager = userManager;
            this.encriptionManager = encriptionManager;
        }


        public User? Login(string email, string password)
        {
            var user = userManager.GetUserByEmail(email);

            if (user == null)
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

            user.Password = encriptionManager.HashPassword(password);

            userManager.AddUser(user);
        }

        public void Update(User user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Password = encriptionManager.HashPassword(password);
            userManager.UpdateUser(user);
        }
    }
}
