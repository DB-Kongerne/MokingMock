namespace MockServers.Interfaces
{
    using MockServers.Model;
    // Services/UserServiceClient.cs
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<User>($"http://localhost:5000/users/{userId}");
        }
    }

}
