

using OrdersWebAPI2.Models;

namespace OrdersWebAPI2.Repositories
{
    public class OrderRepository
    {
       
            private readonly List<Order> _orders = new List<Order>();   //new();

            public OrderRepository()
            {
            _orders.Add(new Order() { Id = 1,  Quantity=3 , ProductName="Banana", UserId=1 });
            _orders.Add(new Order() { Id = 2, Quantity = 2, ProductName = "salat", UserId = 2 });
            _orders.Add(new Order() { Id = 3, Quantity = 12, ProductName = "Apple", UserId = 1 });
            _orders.Add(new Order() { Id = 4, Quantity = 1, ProductName = "Carrot", UserId = 1 });
        }

            // Get all orders
            public virtual IEnumerable<Order> GetAllOrders()
            {
                return _orders;
            }

            // Get orders by User ID
            public IEnumerable<Order> GetOrdersById(int id)
            {
                return _orders.Where(u => u.UserId == id).ToList();
            }

        public Order GetOrderById(int id)
        {
            return _orders.Find(u => u.Id == id);
        }

        // Add a new order
        public virtual void AddOrder(Order order)
            {
                order.Id = _orders.Any() ? _orders.Max(u => u.Id) + 1 : 1;
               _orders.Add(order);
            }

            // Update existing user
            public bool UpdateOrder(Order order)
            {
                var existingOrder = GetOrderById(order.Id);
                if (existingOrder == null)
                {
                    return false;
                }

                existingOrder.ProductName = order.ProductName;
            existingOrder.Quantity = order.Quantity;
            existingOrder.Id = order.Id;


            existingOrder.UserId = order.UserId;
                return true;
            }

            // Delete user by ID
            public bool DeleteOrder(int id)
            {
                var order = GetOrderById(id);
                if (order == null)
                {
                    return false;
                }

                _orders.Remove(order);
                return true;
            }
        }
    }

