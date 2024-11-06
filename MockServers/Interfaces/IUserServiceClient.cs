using MockServers.Model;

namespace MockServers.Interfaces
{
    // IUserServiceClient.cs
    public interface IUserServiceClient
    {
        Task<User> GetUserAsync(int userId);
    }
}
