namespace Unit_Testing.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;
        public UserRepository()
        {
            _users = new List<User>
            {
                new User{ Id = 1,Name="John",Email="john@example.com"},
                new User{ Id = 2,Name="James",Email="james@example.com" }
            };
        }
        public User GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }
        public void AddUser(User user)
        {
            _users.Add(user);
        }
        public void UpdateUser(User user)
        {
            var existingUser=GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name= user.Name;
                existingUser.Email= user.Email;
            }
        }
        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            if(user != null)
            {
                _users.Remove(user);
            }
        }
    }
}