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

        public OrderController(Cart cart)
        {
            order.Cart = cart;
            order.TotalPrice = cart.TotalPrice;
        }

        public void RunOrderController()
        {
            // view.PrintCart();
            SetPaymentMethod();
            SetDeliveryOption();

            PlaceOrder();
        }

        private void SetPaymentMethod()
        {
            view.PrintPayments(orderDao.GetAllPaymentMethods());
            int selectedPaymentId = Int32.Parse(view.GetUserInput("Select payment method (choose number): "));
            Payment selectedPayment = orderDao.GetPaymentMethod(selectedPaymentId);
            order.Payment = new Payment(selectedPayment.Id, selectedPayment.Name, selectedPayment.Cost);

        }

        private void SetDeliveryOption()
        {
            view.PrintDeliveries(orderDao.GetAllDeliveryOptions());
            int selectedDeliveryId = Int32.Parse(view.GetUserInput("Select delivery option (choose number): "));
            Delivery selectedDelivery = orderDao.GetDeliveryOption(selectedDeliveryId);
            order.Delivery = new Delivery(selectedDelivery.Id, selectedDelivery.Name, selectedDelivery.Cost);
        }

        private void PlaceOrder()
        {
            string userInput = view.GetUserInput("Press 'y' to confirm placing the order: ");
            if (userInput == "y")
            {
                // Set Customer details
                orderDao.CreateOrder(order);
                view.PrintOrderConfirmation(order);
            }
            else
            {
                view.PrintMessage("Order not confirmed.");
            }
        }
    }
}
