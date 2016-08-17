using Sales.Orders.Domain;

namespace Sales.Orders.Infrastructure
{
    public class OrderRepository
    {
        private int _id = 1;
        public int SaveOrder(int productId, int userId, int shippingTypeId)
        {
            return _id;
        }

        public void ChangeOrderStatus(int orderId, int orderStatus)
        {
            //Some logic to change order status
        }

        public Order GetOrderBy(int orderId)
        {
            //Mock order objext
            return new Order(orderId).ToUser(12).WithProduct(1).WithProduct(2);
        }
    }
}
