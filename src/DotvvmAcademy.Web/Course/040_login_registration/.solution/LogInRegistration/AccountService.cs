using System.Collections.Generic;

namespace DotvvmAcademy.Course.LogInRegistration
{
    /// <summary>
    /// Simulates database access to an account table.
    /// <remarks>
    /// WARNING: Is NOT secure. Stores passwords in plain text. That is a very bad pracice!
    /// </remarks>
    /// </summary>
    public class AccountService
    {
        private static readonly Dictionary<string, User> Users = new Dictionary<string, User>();

        public bool LogIn(string email, string password)
        {
            if (Users.TryGetValue(email, out var user))
            {
                return user.Password == password;
            }
            return false;
        }

        public bool Register(string email, string password, string name, int age)
        {
            if (Users.ContainsKey(email))
            {
                return false;
            }
            Users.Add(email, new User { Age = age, Name = name, Password = password });
            return true;
        }

        private class User
        {
            public int Age { get; set; }

            public string Name { get; set; }

            public string Password { get; set; }
        }
    }
}