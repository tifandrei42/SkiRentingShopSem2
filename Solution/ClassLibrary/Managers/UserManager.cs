using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
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

        /// <summary>
        /// Retrieves a user by their ID via the mediator.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null.</returns>
        public User GetUserById(int userId)
        {
            return _userMediator.GetUserById(userId);
        }

        /// <summary>
        /// Retrieves a user by their email address via the mediator.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null.</returns>
        public User GetUserByEmail(string email)
        {
            return _userMediator.GetUserByEmail(email);
        }

        /// <summary>
        /// Retrieves a list of users by their role via the mediator.
        /// </summary>
        /// <param name="role">The UserRole to filter by.</param>
        /// <returns>A list of User objects with the specified role.</returns>
        public List<User> GetUsersByRole(UserRole role)
        {
            return _userMediator.GetUsersByRole(role);
        }
    }
}
