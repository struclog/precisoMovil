using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisoGps.Models
{
    public class LoginResponse
    {
        public string message { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
}
