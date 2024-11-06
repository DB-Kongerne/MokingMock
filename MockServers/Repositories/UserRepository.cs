using MockServers.Model;

namespace MockServers.Repositories
{
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>();   //new();

        public UserRepository() 
        { 
            _users.Add(new User() { UserId=1 , Email="moal@eaaa.dk", Name="Mohammed"});
            _users.Add(new User() { UserId= 2, Email = "pelel@eaaa.dk", Name = "Peter" });
        }

        // Get all users
        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        // Get user by ID
        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }

        // Add a new user
        public void AddUser(User user)
        {
            user.UserId = _users.Any() ? _users.Max(u => u.UserId) + 1 : 1;
            _users.Add(user);
        }

        // Update existing user
        public bool UpdateUser(User user)
        {
            var existingUser = GetUserById(user.UserId);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
          
            existingUser.Email = user.Email;
            existingUser.UserId = user.UserId;
            return true;
        }

        // Delete user by ID
        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return false;
            }

            _users.Remove(user);
            return true;
        }
    }
}
