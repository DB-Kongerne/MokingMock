using Microsoft.AspNetCore.Mvc;
using MockServers.Interfaces;
using MockServers.Model;
using Moq;
using Moq.Protected;
using OrdersWebAPI2.Controllers;
using OrdersWebAPI2.Models;
using System.Net;
using System.Text.Json;

namespace ShopUnitTests
{
    [TestClass]
    public class OrdersUniTest
    {
        
        [TestMethod]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = 2;
            var expectedUser = new MockServers.Model.User { UserId = userId, Name = "Alice", Email = "alice@example.com" };
            var expectedJson = JsonSerializer.Serialize(expectedUser);
           // var mockHttpClient = new Mock<HttpClient>();
            // Mock the HttpClient response for GeoLocationAPI
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.ToString().Contains($"/api/User/{userId}")
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedJson),
                });

            // Use the same base address as in the application
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:7088") 
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var orderService = new OrdersController(mockHttpClientFactory.Object);
    
            // Act
            var result = await orderService.GetUserWithOrders(userId) as OkObjectResult;

            // assert
            Assert.IsNotNull(result); // Ensure the result is not null
            Assert.AreEqual(200, result.StatusCode); // Check for HTTP 200 OK

            // Convert the Value property to User
            var orders = (List<Order>)result.Value; //as MockServers.Model.User;
            Assert.IsNotNull(orders);
            Assert.AreEqual(1, orders.Count);
            
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            
        }


        [TestMethod]
        public async Task CreateOrder_ShouldReturnOrder_WhenUserIsValid()
        {

        }

        [TestMethod]
        public void TestMethod1() { }
        // other test cases


        [TestMethod]
        public void TestMethod2() { }
        // other test cases


        [TestMethod]
        public void TestMethod3() { }
        // other test cases

        [TestMethod]
        public void TestMethod4() { }
        // other test cases


        [TestMethod]
        public void TestMethod5() { }
        // other test cases

        [TestMethod]
        public void TestMethod6() { }
        // other test cases


        [TestMethod]
        public void TestMethod7() { }
        // other test cases

       
        [TestMethod]
        public void TestMethod8() { }
        // other test cases

    }
}
