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
using System.Data;
using BL.Component.Common;
using SBBL.Component.Session;

namespace SBBL.Dao.Modules.Shop
{
    public class ProductDao : BaseDAO<ProductDao>, IProduct<EProduct>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void InsertBySyncFB(long shopID, string albumFbId, string albumName , IList<string> urlList, IList<string> nameList, IList<string> noteList, int count)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = @"insert sb_product (shop_id, product_code, product_name, image_url, note, active, is_deleted
                                                          , created_by, created_date, updated_by, updated_date)
                                                     values(@shop_id, 'Auto', @product_name, @image_url, @note, 'N', 'N'
                                                          , @created_by, @created_date, @updated_by, @updated_date)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        for (int i = 0; i < count; i++)
                        {
                            if (IsUniqeByUrl(shopID, urlList[i]))
                            {
                                param.Clear();
                                base.AddSQLParam(param, "@shop_id", shopID);
                                base.AddSQLParam(param, "@product_name", nameList[i]);
                                base.AddSQLParam(param, "@image_url", urlList[i]);
                                base.AddSQLParam(param, "@note", noteList[i]);
                                base.AddCreateUpdate(param);

                                cmd.ExecuteNonQuery();
                                long id = BLUtil.NVLLong(base.GetIdentity(con));
                                base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                    , string.Format("insert sb_product shop_id/product_id = {0}/{1}", shopID, id), con);
                            }
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

        public void Insert(long shopID, IList<EProduct> objList)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = @"insert sb_product (product_id, shop_id, product_code, product_name, price
                                                          , promotionPrice, qty, is_qty, is_preorder, image_url, active
                                                          , created_by, created_date, updated_by, updated_date)
                                                     values(@product_id, @shop_id, @product_code, @product_name, @price
                                                          , @promotionPrice, @qty, @is_qty, @is_preorder, @image_url, @active
                                                          , @created_by, @created_date, @updated_by, @updated_date)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        foreach (EProduct obj in objList)
                        {
                            param.Clear();
                            SetParameter(param, obj, shopID);
                            base.AddCreateUpdate(param);

                            cmd.ExecuteNonQuery();
                            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("insert sb_product shop_id/product_id = {0}/{1}", shopID, obj.productID), con);
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

        public void Update(long shopID, IList<EProduct> objList)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = @"update sb_product set product_id = @product_id
                                                             , shop_id = @shop_id
                                                             , product_code = @product_code
                                                             , product_name = @product_name
                                                             , price = @price
                                                             , promotionPrice = @promotionPrice
                                                             , is_qty = @is_qty
                                                             , image_url = @image_url
                                                             , active = @active
                                                             , updated_by = @updated_by
                                                             , updated_date = @updated_date
                                      where shop_id=@shop_id and product_id=@product_id";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        foreach (EProduct obj in objList)
                        {
                            param.Clear();
                            SetParameter(param, obj, shopID);
                            cmd.ExecuteNonQuery();
                            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("update sb_product shop_id/product_id = {0}/{1}", shopID, obj.productID), con);
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

        public void Delete(long shopID, EProduct obj)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = "delete from sb_product where shop_id = @shop_id and product_id = @product_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    param.Clear();
                    base.AddSQLParam(param, "@shop_id", shopID);
                    base.AddSQLParam(param, "@product_id", obj.productID);

                    cmd.ExecuteNonQuery();
                    base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("delete sb_product shop_id/product_id = {0}/{1}", shopID, obj.productID), con);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void UpdateIsActive(long shopID, IList<long> productIDList)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = @"update sb_product
                                      set active = @active
                                   where shop_id = @shop_id and product_id = @product_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    foreach (long productID in productIDList)
                    {
                        param.Clear();
                        base.AddSQLParam(param, "@shop_id", shopID);
                        base.AddSQLParam(param, "@product_id", productID);
                        cmd.ExecuteNonQuery();

                        base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("update active sb_product shop_id/product_id = {0}/{1}", shopID, productID), con);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public IList<EProduct> GetAllByShopID(long shopID)
        {
            try
            {
                string sql = @"select *
                                from sb_product
                                where shop_id = @shop_id
                                order by created_date desc";
                DataTable dt = GetDataTable(sql, "@shop_id", shopID);
                List<EProduct> objList = new List<EProduct>();
                foreach (DataRow dto in dt.Rows)
                {
                    EProduct obj = new EProduct();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.productID = BLUtil.NVLLong(dto["bank_id"]);
                    obj.productCode = BLUtil.NVLString(dto["bank_code"]);
                    obj.productName = BLUtil.NVLString(dto["bank_name"]);
                    obj.price = BLUtil.NVLDecimal(dto["aaa"]);
                    obj.promotionPrice = BLUtil.NVLDecimal(dto["aaa"]);
                    obj.qty = BLUtil.NVLInt(dto["qty"]);
                    obj.isQty = BLUtil.NVLString(dto["is_qty"]);
                    obj.isPreOrder = BLUtil.NVLString(dto["is_preorder"]);
                    obj.imageUrl = BLUtil.NVLString(dto["image_url"]);
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

        public EProduct GetByID(long shopID,long productID)
        {
            try
            {
                string sql = @"select *
                                from sb_product
                                where isnull(IS_DELETED,'N') = 'N'
                                  and shop_id = @shop_id and product_id = @product_id";
                DataTable dt = GetDataTable(sql, new string[] { "@shop_id" }, new object[] { productID });
                if (dt.Rows.Count > 0)
                {
                    DataRow dto = dt.Rows[0];
                    EProduct obj = new EProduct();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.productID = BLUtil.NVLLong(dto["product_id"]);
                    obj.productCode = BLUtil.NVLString(dto["product_code"]);
                    obj.productName = BLUtil.NVLString(dto["product_name"]);
                    obj.price = BLUtil.NVLDecimal(dto["price"]);
                    obj.promotionPrice = BLUtil.NVLDecimal(dto["promotion_price"]);
                    obj.qty = BLUtil.NVLInt(dto["qty"]);
                    obj.isQty = BLUtil.NVLString(dto["is_qty"]);
                    obj.isPreOrder = BLUtil.NVLString(dto["is_preorder"]);
                    obj.imageUrl = BLUtil.NVLString(dto["image_url"]);
                    obj.active = BLUtil.NVLString(dto["active"]);
                    obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                    obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                    obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                    obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                    return obj;
                }
                else
                {
                    return new EProduct();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public bool IsUniqe(long shopID, string productCode)
        {
            try
            {
                int count = 0;
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = @"select count(product_id)
                                   from sb_product
                                   where shop_id = @shop_id and product_code = @product_code and isnull(is_deleted,'N') = 'N'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                        param.Clear();
                        base.AddSQLParam(param, "@shop_id", shopID);
                        base.AddSQLParam(param, "@product_code", productCode);
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
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        private void SetParameter(SqlParameterCollection param, EProduct obj, long shopID)
        {
            base.AddSQLParam(param, "@shop_id", shopID);
            base.AddSQLParam(param, "@product_code", obj.productCode);
            base.AddSQLParam(param, "@product_name", obj.productName);
            base.AddSQLParam(param, "@price", obj.price);
            base.AddSQLParam(param, "@promotionPrice", obj.promotionPrice);
            base.AddSQLParam(param, "@qty", obj.qty);
            base.AddSQLParam(param, "@is_qty", obj.isQty);
            base.AddSQLParam(param, "@is_preorder", obj.isPreOrder);
            base.AddSQLParam(param, "@image_url", obj.imageUrl);
            base.AddSQLParam(param, "@active", obj.active);
        }

        private bool IsUniqeByUrl(long shopID, string url)
        {
            try
            {
                int count = 0;
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = @"select count(product_id)
                                   from sb_product
                                   where shop_id = @shop_id and image_url = @image_url and isnull(is_deleted,'N') = 'N'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    param.Clear();
                    base.AddSQLParam(param, "@shop_id", shopID);
                    base.AddSQLParam(param, "@image_url", url);
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
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
