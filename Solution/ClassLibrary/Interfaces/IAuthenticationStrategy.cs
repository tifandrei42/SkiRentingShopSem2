using BusinessLogic.Entities;
using BusinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthenticationStrategy
    {
        UserManager userManager { get; set; }

        EncriptionManager encriptionManager { get; set; }

        User Login(string email, string password);
        void Register(User user, string password);
    }
}
