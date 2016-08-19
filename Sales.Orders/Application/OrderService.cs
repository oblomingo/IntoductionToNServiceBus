using System;
using Logistic.Messages.Commands;
using Logistic.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using Sales.Orders.Domain;
using Sales.Orders.Infrastructure;
using Stock.Messages.Commands;
using Stock.Messages.Events;

namespace Sales.Orders.Application
{
    public class OrderService : IHandleMessages<PlaceOrderCmd>, 
        IHandleMessages<PackagePrepared>, 
        IHandleMessages<OutOfStock>, 
        IHandleMessages<PackageSent>
    {
        private readonly ILog _log;
        private readonly OrderRepository _orderRepository;
        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _log = LogManager.GetLogger(typeof(OrderService));
        }

        public IBus Bus { get; set; }

        public void Handle(PlaceOrderCmd message)
        {
            var orderId = _orderRepository.SaveOrder(message.ProductId, message.UserId, message.ShippingTypeId);

            _log.InfoFormat("Order created for user id {0} with product id {1}", message.UserId, message.ProductId);

            var preparePackage = new PreparePackageCmd
            {
                OrderId = orderId,
                ProductId = message.ProductId
            };

            Bus.Send(preparePackage);
        }

        public void Handle(PackagePrepared message)
        {
            _orderRepository.ChangeOrderStatus(message.OrderId, (int)Enums.OrderStatuses.PackagePrepared);
            var order = _orderRepository.GetOrderBy(message.OrderId);

            if (order != null)
            {
                var sendPackage = new SendPackageCmd
                {
                    OrderId = message.OrderId,
                    PackageId = message.PackageId,
                    Size = message.Size,
                    UserId = order.UserId
                };
                Bus.Send(sendPackage);
            }
        }

        public void Handle(OutOfStock message)
        {
            _orderRepository.ChangeOrderStatus(message.OrderId, (int)Enums.OrderStatuses.OutOfStock);
        }

        public void Handle(PackageSent message)
        {
            _orderRepository.ChangeOrderStatus(message.OrderId, (int)Enums.OrderStatuses.Complete);

            var order = _orderRepository.GetOrderBy(message.OrderId);
            var saleCompleted = new SaleCompleted
            {
                UserId = order.UserId,
                ProductId = order.ProductId,
                Date = DateTime.UtcNow,
            };
            Bus.Publish(saleCompleted);
        }
    }
}
