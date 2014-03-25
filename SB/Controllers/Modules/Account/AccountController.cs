using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBBL.Component.Entity;
using SBBL.Component.Session;

namespace SB.Controllers.Modules.Account
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SetLoginByFacebook(string id, string name, string url, string image)
        {
            try
            {
                //TODO prepare data for user
                ELogin objLogin = new ELogin();
                EFb objFb = new EFb();
                objFb.FbID = id;
                objFb.FbName = name;
                objFb.Link = url;
                objFb.Image = image;
                objLogin.objFb = objFb;
                Login.User = objLogin;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        [HttpPost]
        public JsonResult GetLoginByFacebook()
        {
            try
            {
                return Json(Login.User.objFb);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public void SetLogout()
        {
            try
            {
                Login.User = null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

    }
}
