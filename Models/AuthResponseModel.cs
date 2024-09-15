using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AuthResponseModel
    {
        public AuthResponseModel(string userId, string userName, string token)
        {
            UserId = userId;
            UserName = userName;
            Token = token;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token {  get; set; }
             
    }
}
