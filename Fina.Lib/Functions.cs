using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace Fina.Lib
{
    class Functions
    {
        public bool LoggedIn()
        {
            if (HttpContext.Session.GetString("User") != null)
            {

                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                // int? userSession = HttpContext.Session.GetInt32("Id");

                if (userSession.Id != 0)
                {

                }
            }
        }
    }
}
