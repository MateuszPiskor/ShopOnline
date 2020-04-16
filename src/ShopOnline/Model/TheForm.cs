using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Model
{
    public class TheForm
    {

        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal string Email { get; set; }
        internal string RequestType { get; set; }
        internal string RequestText { get; set; }
        //internal DateTime DateAndTime;

        public TheForm( string firstName , string lastName , string email , string requestType , string requestText )
        {
            //this.DateAndTime = DateTime.Now;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.RequestType = requestType;
            this.RequestText = requestText;
        }
    }
}
