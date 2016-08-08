using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        public static Queue<SaleCompleted> PaymentAcceptedOrderQueue = new Queue<SaleCompleted>();

        public ActionResult Index()
        {
            return Json(new { text = "Order Controller" });
        }

        public ActionResult Send(int productId, int shippingTypeId)
        {

            var placeOrder = new PlaceOrderCmd
            {
                ProductId = productId,
                UserId = GetUserId(),
                ShippingTypeId = shippingTypeId,
                TimeStamp = DateTime.UtcNow
            };

            ServiceBus.Bus.Send(placeOrder);

            return Json(new { sent = placeOrder });
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentAcceptedOrders()
        {
            return Json(PaymentAcceptedOrderQueue.ToArray());
        }

        private int GetUserId()
        {
            //Hardcoded user Id
            return 12;
        }
    }
}