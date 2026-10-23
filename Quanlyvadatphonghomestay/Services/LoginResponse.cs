using System;
using System.Collections.Generic;
using System.Text;

namespace Quanlyvadatphonghomestay.Services
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
