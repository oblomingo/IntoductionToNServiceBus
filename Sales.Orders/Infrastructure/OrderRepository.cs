using System.Collections.Generic;
using System.Linq;
using Sales.Orders.Domain;

namespace Sales.Orders.Infrastructure
{
    public class OrderRepository
    {
        private readonly List<Order> _orders = new List<Order>(); 
        private int _id = 0;
        public int SaveOrder(int productId, int userId, int shippingTypeId)
        {
            _id++;
            var newOrder = new Order(_id)
                .ToUser(userId)
                .WithProduct(productId)
                .Using(shippingTypeId);

            _orders.Add(newOrder);
            return _id;
        }

        public void ChangeOrderStatus(int orderId, int orderStatus)
        {
            var order = _orders.SingleOrDefault(o => o.OrderId == orderId);
            order?.ChangeStatus(orderStatus);
        }

        public Order GetOrderBy(int orderId)
        {
            return _orders.SingleOrDefault(o => o.OrderId == orderId);
        }
    }
}
