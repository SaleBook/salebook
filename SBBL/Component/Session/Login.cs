using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SBBL.Component.Entity;

namespace SBBL.Component.Session
{
    public static class Login
    {
        public static ELogin User
        {
            get
            {
                return (ELogin)HttpContext.Current.Session["user"];
            }
            set
            {
                HttpContext.Current.Session["user"] = value;
            }
        }
    }
}
