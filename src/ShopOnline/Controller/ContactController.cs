using System;
using System.Collections.Generic;
using System.Text;
using ShopOnline.Model;
using ShopOnline.DataAccess;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    internal class ContactController:AboutUs
    {
        private readonly string[] RequestTypes = new string[4] { "complaint" , "idea" , "opinion" , "question" };
      
        internal TheForm newForm;
        View newView = new View();

        public ContactController()
        {
            ChooseContactOption();
        }
        public void ChooseContactOption()
        {
            ShowContactOptions();
            string message = "As you consider sending a message to us fill free to press 'f' letter" +
                "\n If you prefer coming back to Main Menu press 'q' ";
            var inputLetter = newView.GetUserInput(message);
            if (inputLetter == "f")
            {
                newForm = GetForm();
                new ContactDaoDB().SendToDB(newForm); 
            }
        }
        private string GetInput()
        {
            return Console.ReadLine();
        }

        #region FillingTheForm
        private void ShowRequestOptions()
        {
            char i = 'a';
            System.Collections.IEnumerator enumArray = RequestTypes.GetEnumerator();
            Console.WriteLine("Choose the one of following cases that you would like to write about to us:");
            while ( ( enumArray.MoveNext() ) && ( enumArray.Current != null ) )
            {
                Console.WriteLine("({0}) {1}" , i++ , enumArray.Current);
            }
        }
        private string GetRequestType()
        {
            var requestChoice = GetInput();

            switch ( requestChoice )
            {
                case "a":
                    return RequestTypes[0];
                case "b":
                    return RequestTypes[1];
                case "c":
                    return RequestTypes[2];
                case "d":
                    return RequestTypes[3];
            }
            return "";
        }

        private bool IsValidEmail( string email )
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private string GetValidEmailStatus()
        {
            var IsCorrect = false;
            while ( IsCorrect == false )
            {
                var message = "Provide your e-mail:";
                var item = newView.GetUserInput(message);
                if ( IsValidEmail(item) )
                {
                    return item;
                }
            }
            return "";
        }
        private TheForm GetForm()
        {
            var firstNamePrompt = "Provide your First Name: ";
            var lastNamePrompt = "Provide your Last Name: ";
            var requestTextPrompt = "Write what you want to tell us:";
            ShowRequestOptions();
            var request = GetRequestType();
            var firstName = newView.GetUserInput(firstNamePrompt);
            var lastName = newView.GetUserInput(lastNamePrompt);
            var email = GetValidEmailStatus();
            var requestText = newView.GetUserInput(requestTextPrompt);
            return new TheForm(firstName , lastName , email , request , requestText);
        }
        #endregion
    }
}
