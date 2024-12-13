using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IUserMediator
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        List<User> GetUsersByRole(UserRole role);
    }
}
