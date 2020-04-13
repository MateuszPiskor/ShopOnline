using System;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class OrderController
    {
        public bool IsActive { get; set; }
        View view = new View();
        Order order = new Order();
        IOrderDao orderDao = new OrderDaoDB();

        public OrderController()
        {
        }

        public void RunOrderController()
        {
            // view.PrintCart();
            SetPaymentMethod();

        }

        private void SetPaymentMethod()
        {
            view.PrintPayments(orderDao.GetAllPaymentMethods());
            int selectedPaymentId = Int32.Parse(view.GetUserInput("Select payment method (choose number): "));
            Payment selectedPayment = orderDao.GetPaymentMethod(selectedPaymentId);
            order.Payment = new Payment(selectedPayment.Id, selectedPayment.Name, selectedPayment.Cost);

        }
    }
}
