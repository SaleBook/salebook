using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS.Controllers;
using SBBL.Dto.Modules.Shop;
using SBBL.Dto.Modules.Master;
using SBBL.Dao.Modules.Shop;
using SBBL.Component.Session;
using BL.Component.Common;
using SBBL.Dao.Modules.Master;
using SBBL.Component.Entity;

namespace SB.Controllers.Modules.Shop
{
    public class ShopController : Controller
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Shop/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SetPageData(string id, string name, string url, string image)
        {
            try
            {
                //TODO prepare data for shop
                if (Login.User != null && Login.User.objFb != null)
                {
                    EFanPage obj = new EFanPage();
                    obj.FanPageID = id;
                    obj.FanPageName = name;
                    obj.FanPageLink = url;
                    obj.FanPageImageUrl = image;
                    Login.User.objFb.FanPage = obj;

                    string msg = string.Empty;
                    AccountDao.Instance.SetUserSeller(Login.User.objFb, ref msg);
                    return Url.Content("~/Shop/Order");
                }

                return "";
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetPageData()
        {
            return Json(Login.User.objFb.FanPage);
        }

        public ActionResult Order()
        {
            return View();
        }

        // shop bank
        public ActionResult Bank()
        {
            try
            {
                EShopBank obj = new EShopBank();
                obj.shopBankList = ShopBankDao.Instance.GetAllByShopID(Login.User.ShopID);
                obj.bankList = ShopBankDao.Instance.GetBankNotExists(Login.User.ShopID);

                if (obj.bankList.Count > 0)
                {
                    EBank bank = obj.bankList[0];
                    obj.bankID = bank.bankID;
                    obj.bankCode = bank.bankCode;
                    obj.bankName = bank.bankName;
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Bank(EShopBank obj)
        {
            try
            {
                List<EShopBank> objList = new List<EShopBank>();
                obj.shopID = Login.User.ShopID;
                obj.bankID = BLUtil.NVLLong(Request["hidBankID"]);
                obj.active = "Y";
                objList.Add(obj);
                ShopBankDao.Instance.Insert(Login.User.ShopID, objList);
                ModelState.Clear();

                return Bank();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public string DeleteShopBank(string BankID)
        {
            try
            {
            EShopBank obj = new EShopBank();
            obj.shopID = Login.User.ShopID;
            obj.bankID = BLUtil.NVLLong(BankID);
            ShopBankDao.Instance.Delete(Login.User.ShopID, obj);
            return Url.Content("~/Shop/Bank");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        // shop delivery
        public ActionResult Delivery()
        {
            try
            {
                EShopDelivery obj = new EShopDelivery();
                obj.ShopDeliveryList = ShopDeliveryDao.Instance.GetAllByShopID(Login.User.ShopID);
                obj.DeliveryList = DeliveryDao.Instance.GetIsActive();

                if (obj.DeliveryList.Count > 0)
                {
                    EDelivery delivery = obj.DeliveryList[0];
                    obj.deliveryID = delivery.deliveryID;
                    obj.deliveryCode = delivery.deliveryCode;
                    obj.deliveryName = delivery.deliveryName;
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Delivery(EShopDelivery obj)
        {
            try
            {
                if (ShopDeliveryDao.Instance.IsUniqe(Login.User.ShopID, obj.deliveryID, obj.moreQty))
                {
                    List<EShopDelivery> objList = new List<EShopDelivery>();
                    obj.shopID = Login.User.ShopID;
                    obj.active = "Y";
                    objList.Add(obj);
                    ShopDeliveryDao.Instance.Insert(Login.User.ShopID, objList);
                    ModelState.Clear();
                    return Delivery();
                }
                else
                {
                    ModelState.AddModelError("deliveryID", "ข้อมูลเคยมีการเพิ่มแล้ว");
                    ModelState.AddModelError("moreQty", "ข้อมูลเคยมีการเพิ่มแล้ว");

                    obj.ShopDeliveryList = ShopDeliveryDao.Instance.GetAllByShopID(Login.User.ShopID);
                    obj.DeliveryList = DeliveryDao.Instance.GetIsActive();

                    if (obj.DeliveryList.Count > 0)
                    {
                        EDelivery delivery = obj.DeliveryList[0];
                        obj.deliveryID = delivery.deliveryID;
                        obj.deliveryCode = delivery.deliveryCode;
                        obj.deliveryName = delivery.deliveryName;
                    }

                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public string DeleteShopDelivery(string deliveryID, string moreQty)
        {
            try
            {
                EShopDelivery obj = new EShopDelivery();
                obj.shopID = Login.User.ShopID;
                obj.deliveryID = BLUtil.NVLLong(deliveryID);
                obj.moreQty = BLUtil.NVLInt(moreQty);
                ShopDeliveryDao.Instance.Delete(Login.User.ShopID, obj);
                return Url.Content("~/Shop/Delivery");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        // product
        public ActionResult Product()
        {
            try
            {
                EProduct obj = new EProduct();
                //obj.productList = ProductDao.Instance.GetAllByShopID(Login.User.ShopID);
                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

    }
}
