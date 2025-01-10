using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionTest.Utils
{
    public class FakeUserMediator : IUserMediator
    {
        private readonly Dictionary<int, User> _users;

        public FakeUserMediator()
        {
            _users = new Dictionary<int, User>();
        }

        public void AddUser(User user)
        {
            if (_users.ContainsKey(user.User_Id))
                throw new ArgumentException("User with the same ID already exists.");

            _users.Add(user.User_Id, user);
        }

        public void UpdateUser(User user)
        {
            if (!_users.ContainsKey(user.User_Id))
                throw new KeyNotFoundException("User not found.");

            _users[user.User_Id] = user;
        }

        public void DeleteUser(int userId)
        {
            if (!_users.Remove(userId))
                throw new KeyNotFoundException("User not found.");
        }

        public User GetUserById(int userId)
        {
            if (!_users.TryGetValue(userId, out var user))
                throw new KeyNotFoundException("User not found.");

            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _users.Values.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<User> GetUsersByRole(string role)
        {
            if (!Enum.TryParse<UserRole>(role, out var userRole))
                throw new ArgumentException("Invalid role.");

            return _users.Values.Where(user => user.Role == userRole).ToList();
        }

        public List<User> GetAllUsers()
        {
            return _users.Values.ToList();
        }
    }
}
