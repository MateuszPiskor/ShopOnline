using System;
namespace ShopOnline.DataAccess
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(string msg) : base(msg)
        {
        }
    }
}
