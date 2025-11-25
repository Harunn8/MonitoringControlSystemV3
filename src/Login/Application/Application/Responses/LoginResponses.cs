using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;

namespace LoginApplication.Responses
{
    public class LoginResponses
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public LoginResponses(UsersModel user, string token)
        {
            Token = token;
            Id = user.Id;
            UserName = user.UserName;
        }
    }
}
