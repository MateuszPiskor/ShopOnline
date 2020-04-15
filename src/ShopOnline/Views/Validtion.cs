using System;

namespace ShopOnline.Views
{
    public class Validation
    {
        public Validation()
        {
        }
        public bool isProductNumber(string userAnswer)
        {
            int result;
            bool resultOfValidation = false;
            if((int.TryParse(userAnswer, out result) && result>0 && result<401))
            {
                resultOfValidation = true;
            }
            return resultOfValidation; 
        }
        public bool isProductNumberDescriptionRequest(string userAnswer)
        {
            int result;
            string [] splitedAnswer=userAnswer.Split("+");
            bool resultOfValidation = false;
            if ((int.TryParse(splitedAnswer[0], out result) && result > 0 && result < 411))
            {
                resultOfValidation = true;
            }
            return resultOfValidation;
        }
    }
}