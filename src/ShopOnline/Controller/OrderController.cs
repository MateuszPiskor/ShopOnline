﻿using System;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class OrderController
    {
        public bool IsActive { get; private set; }
        View view = new View();
        Order order = new Order();
        IOrderDao orderDao = new OrderDaoDB();


        public OrderController(Cart cart)
        {
            order.Cart = cart;
            IsActive = true;
        }

        public void RunOrderController()
        {
            // view.PrintCart();
            SetPaymentMethod();
            SetDeliveryOption();
            SetTotalPrice();
            SetCustomerDetails();
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
            string userInput = view.GetUserInput("Press 'y' to confirm the order: ");
            if (userInput == "y")
            {
                orderDao.CreateOrder(order);
                view.PrintOrderConfirmation(order);
                ProductController productController = new ProductController();
                productController.RunProductController();
            }
            else
            {
                view.PrintMessage("Order not confirmed.");
            }
        }

        private void SetCustomerDetails()
        {
            CustomerController customerController = new CustomerController(new CustomerDaoDB());
            customerController.RunMenu();
            order.Customer = customerController.GetCustomer();
        }

        private void SetTotalPrice()
        {
            order.TotalPrice = order.Cart.TotalPrice + order.Delivery.Cost + order.Payment.Cost;
        }
    }
}
