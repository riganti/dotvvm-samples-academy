using System.Collections.Generic;

namespace DotvvmAcademy.Course.LogIn
{
    public class AccountService
    {
        public bool LogIn(string email, string password)
        {
            // xkcd #936
            return email == "john@example.com" && password == "CorrectHorseBatteryStaple";
        }
    }
}