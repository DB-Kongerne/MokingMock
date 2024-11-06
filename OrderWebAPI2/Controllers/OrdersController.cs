using Microsoft.AspNetCore.Mvc;
using OrdersWebAPI2.Models;
using OrdersWebAPI2.Repositories;
using OrderWebAPI2.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrdersWebAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
       // private static readonly List<Order> Orders = new List<Order>();
        // This controller: Fetches user data and orders from the mock server.
        //Combines user and order data into a single response.

        private readonly HttpClient _httpClient;
        private  OrderRepository _orderRepository;

        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
            _orderRepository = new OrderRepository();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWithOrders(int userId)
        {
            // Fetch user details from the mock User API
           // var userResponse = await _httpClient.GetAsync($"/users/{userId}");
            var userResponse = await _httpClient.GetAsync($"https://localhost:7088/api/User/{userId}");

            
            if (!userResponse.IsSuccessStatusCode)
                return NotFound("User not found");

            var user = await userResponse.Content.ReadAsStringAsync();

            var orders= _orderRepository.GetOrdersById(userId);
            

            return Ok(orders);

           
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            // Fetch user details from the mock User API
            // var userResponse = await _httpClient.GetAsync($"/users/{userId}");
            var userResponse = await _httpClient.GetAsync($"https://localhost:7088/api/User/{orderRequest.UserId}");

            if (!userResponse.IsSuccessStatusCode)
                return NotFound("User not found");           
           //User userObject = JsonSerializer.Deserialize<User>(await userResponse.Content.ReadAsStringAsync());
        

            // Create Order
            var order = new Order
            {
               Id = _orderRepository.GetAllOrders().Count() + 1,
               UserId = orderRequest.UserId,
                ProductName = orderRequest.ProductName,
                Quantity = orderRequest.Quantity
            };
            _orderRepository.AddOrder(order);

            return Ok(new { Message = "Order created", Order = order });
        }
    }
}
