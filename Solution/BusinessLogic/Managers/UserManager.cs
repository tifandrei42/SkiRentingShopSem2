using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Managers
{
    public class UserManager
    {
        private readonly IUserMediator _userMediator;

        public UserManager(IUserMediator iusermediator)
        {
            _userMediator = iusermediator;
        }

        public void AddUser(User user)
        {
            try
            {
                _userMediator.AddUser(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error adding user: " + ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _userMediator.UpdateUser(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error updating user: " + ex.Message);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                _userMediator.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error deleting user: " + ex.Message);
            }
        }

        public User GetUserById(int userId)
        {
            return _userMediator.GetUserById(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _userMediator.GetUserByEmail(email);
        }

        public List<User> GetUsersByRole(string rolename)
        {
            return _userMediator.GetUsersByRole(rolename);
        }

        public List<User> GetAllUsers()
        {
            return _userMediator.GetAllUsers();
        }
    }
}
