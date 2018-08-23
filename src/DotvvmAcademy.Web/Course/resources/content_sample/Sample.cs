using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course
{
    public class LoginDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegistrationDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
