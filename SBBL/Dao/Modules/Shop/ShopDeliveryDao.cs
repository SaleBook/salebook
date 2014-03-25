using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using SBBL.Dao;
using BL.Dao;
using SBBL.Dto.Modules.Master;
using SBBL.Dto.Modules.Shop;
using BL.Component.Common;
using SBBL.Component.Session;
using System.Data;

namespace SBBL.Dao.Modules.Shop
{
    public class ShopDeliveryDao : BaseDAO<ShopDeliveryDao>, IShopDelivery<EShopDelivery>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void Insert(long shopID, IList<EShopDelivery> objList)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = @"insert sb_shop_delivery (shop_id, delivery_id, more_qty, price
                                                          , active, created_by, created_date, updated_by, updated_date)
                                                     values(@shop_id, @delivery_id, @more_qty, @price 
                                                          , @active, @created_by, @created_date, @updated_by, @updated_date)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        foreach (EShopDelivery obj in objList)
                        {
                            param.Clear();
                            SetParameter(param, obj, shopID);
                            cmd.ExecuteNonQuery();
                            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("insert sb_shop_delivery shop_id/delivery/more_qty = {0}/{1}/{2}", shopID, obj.deliveryID, obj.moreQty), con);
                        }
                    }

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void Update(long shopID, IList<EShopDelivery> objList)
        {
            throw new NotImplementedException();
        }

        public void Delete(long shopID, EShopDelivery obj)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = "delete from sb_shop_delivery where shop_id = @shop_id and delivery_id = @delivery_id and more_qty = @more_qty";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    param.Clear();
                    base.AddSQLParam(param, "@shop_id", shopID);
                    base.AddSQLParam(param, "@delivery_id", obj.deliveryID);
                    base.AddSQLParam(param, "@more_qty", obj.moreQty);

                    cmd.ExecuteNonQuery();
                    base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("delete sb_shop_delivery shop_id/deliveryId/more_qty = {0}/{1}/{2}", shopID, obj.deliveryID, obj.moreQty), con);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void UpdateIsActive(long shopID, IList<EShopDelivery> objList)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = @"update sb_shop_delivery 
                                      set active = @active
                                   where shop_id = @shop_id and delivery_id = @delivery_id and more_qty = @more_qty";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    foreach (EShopDelivery obj in objList)
                    {
                        param.Clear();
                        base.AddSQLParam(param, "@shop_id", shopID);
                        base.AddSQLParam(param, "@delivery_id", obj.deliveryID);
                        base.AddSQLParam(param, "@more_qty", obj.moreQty);
                        cmd.ExecuteNonQuery();

                        base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("update active sb_shop_delivery shop_id/delivery_id/more_qty = {0}/{1}/{2}", shopID, obj.deliveryID, obj.moreQty), con);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public IList<EShopDelivery> GetAllByShopID(long shopID)
        {
            try
            {
                string sql = @"select sd.*
	                                , d.delivery_code
	                                , d.delivery_name
                                from sb_shop_delivery sd, sb_delivery d
                                where sd.delivery_id = d.delivery_id
                                  and isnull(sd.active,'Y') = 'Y'
                                  and sd.shop_id = @shop_id
                                order by sd.delivery_id, sd.more_qty";
                DataTable dt = GetDataTable(sql, "@shop_id", shopID);
                List<EShopDelivery> objList = new List<EShopDelivery>();
                foreach (DataRow dto in dt.Rows)
                {
                    EShopDelivery obj = new EShopDelivery();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.deliveryID = BLUtil.NVLLong(dto["delivery_id"]);
                    obj.deliveryCode = BLUtil.NVLString(dto["delivery_code"]);
                    obj.deliveryName = BLUtil.NVLString(dto["delivery_name"]);
                    obj.moreQty = BLUtil.NVLInt(dto["more_qty"]);
                    obj.price = BLUtil.NVLDecimal(dto["price"]);
                    obj.active = BLUtil.NVLString(dto["active"]);
                    obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                    obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                    obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                    obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                    objList.Add(obj);
                }

                return objList;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public EShopDelivery GetByID(long shopID, long deliveryID, int? moreQty)
        {
            try
            {
                string sql = @"select sd.*
	                                , d.delivery_code
	                                , d.delivery_name
                                from sb_shop_delivery sd, sb_delivery d
                                where sd.delivery_id = d.delivery_id
                                  and sd.shop_id = @shop_id
                                  and sd.delivery_id = @delivery_id
                                  and sd.more_qty = @more_qty";
                DataTable dt = GetDataTable(sql, new string[] { "@shop_id", "@delivery_id", "@more_qty" }, new object[] { shopID, deliveryID, moreQty });
                if (dt.Rows.Count > 0)
                {
                    DataRow dto = dt.Rows[0];
                    EShopDelivery obj = new EShopDelivery();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.deliveryID = BLUtil.NVLLong(dto["delivery_id"]);
                    obj.deliveryCode = BLUtil.NVLString(dto["delivery_code"]);
                    obj.deliveryName = BLUtil.NVLString(dto["delivery_name"]);
                    obj.moreQty = BLUtil.NVLInt(dto["more_qty"]);
                    obj.price = BLUtil.NVLDecimal(dto["price"]);
                    obj.active = BLUtil.NVLString(dto["active"]);
                    obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                    obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                    obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                    obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                    return obj;
                }
                else
                {
                    return new EShopDelivery();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public bool IsUniqe(long shopID, long deliveryID, int? moreQty)
        {
            int count = 0;
            using (SqlConnection con = base.CreateConnection())
            {
                con.Open();
                string sql = @"select count(shop_id)
                                from sb_shop_delivery 
                                where shop_id = @shop_id
                                  and delivery_id = @delivery_id
                                  and more_qty = @more_qty";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlParameterCollection param = cmd.Parameters;
                param.Clear();

                AddSQLParam(param, "@shop_id", shopID);
                AddSQLParam(param, "@delivery_id", deliveryID);
                AddSQLParam(param, "@more_qty", moreQty);

                count = BLUtil.NVLInt(cmd.ExecuteScalar());
            }

            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetParameter(SqlParameterCollection param, EShopDelivery obj, long shopID)
        {
            base.AddSQLParam(param, "@shop_id", shopID);
            base.AddSQLParam(param, "@delivery_id", obj.deliveryID);
            base.AddSQLParam(param, "@more_qty", obj.moreQty);
            base.AddSQLParam(param, "@price", obj.price);
            base.AddSQLParam(param, "@active", obj.active);
            base.AddCreateUpdate(param);
        }
    }
}
