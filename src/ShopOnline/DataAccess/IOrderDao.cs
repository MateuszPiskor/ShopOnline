using System;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public interface IOrderDao
    {
        public Payment GetPaymentMethod(int paymentId);
        Delivery GetDeliveryOption(int deliveryId);
        void ConfirmOrder(Customer customer, Payment payment, Delivery delivery, Cart cart);

    }
}
