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
    public class ShopBankDao : BaseDAO<ShopBankDao>, IShopBank<EShopBank>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public EShopBank GetByID(long shopID, long bankID)
        {
            try
            {
                string sql = @"select sb.*
	                                , b.bank_code
	                                , b.bank_name
	                                , b.image_url
                                from sb_shop_bank sb, sb_bank b
                                where sb.bank_id = b.bank_id
                                  and sb.shop_id = @shop_id
                                  and sb.bank_id = @bank_id";
                DataTable dt = GetDataTable(sql, new string[]{ "@shop_id", "@bank_id"}, new object[]{ shopID, bankID });
                if (dt.Rows.Count > 0)
                {
                    DataRow dto = dt.Rows[0];
                    EShopBank obj = new EShopBank();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.bankID = BLUtil.NVLLong(dto["bank_id"]);
                    obj.bankCode = BLUtil.NVLString(dto["bank_code"]);
                    obj.bankName = BLUtil.NVLString(dto["bank_name"]);
                    obj.imageUrl = BLUtil.NVLString(dto["image_url"]);
                    obj.bookName = BLUtil.NVLString(dto["book_name"]);
                    obj.bookNo = BLUtil.NVLString(dto["bookNo"]);
                    obj.branch = BLUtil.NVLString(dto["branch"]);
                    obj.isMain = BLUtil.NVLString(dto["is_main"]);
                    obj.active = BLUtil.NVLString(dto["active"]);
                    obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                    obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                    obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                    obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                    return obj;
                }
                else
                {
                    return new EShopBank();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public IList<EBank> GetBankNotExists(long shopID)
        {
            try
            {
                string sql = @"select *
                                from sb_bank bank
                                where isnull(active,'Y') = 'Y'
                                  and isnull(is_deleted,'N') = 'N'
                                  and not exists ( select 1 
				                                   from sb_shop_bank a
				                                   where a.bank_id = bank.bank_id 
					                                and a.shop_id = @shop_id)
                                order by bank.bank_code";
                DataTable dt = GetDataTable(sql, "@shop_id", shopID);
                List<EBank> objList = new List<EBank>();
                foreach (DataRow dto in dt.Rows)
                {
                    EBank obj = new EBank();
                    obj.bankID = BLUtil.NVLLong(dto["bank_id"]);
                    obj.bankCode = BLUtil.NVLString(dto["bank_code"]);
                    obj.bankName = BLUtil.NVLString(dto["bank_name"]);
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

        public void Insert(long shopID, IList<EShopBank> objList)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = @"insert sb_shop_bank (shop_id, bank_id, book_name, book_no, branch
                                                          , is_main, active, created_by, created_date, updated_by
                                                          , updated_date)
                                                     values(@shop_id, @bank_id, @book_name, @book_no, @branch
                                                          , @is_main, @active, @created_by, @created_date, @updated_by
                                                          , @updated_date)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        foreach (EShopBank obj in objList)
                        {
                            param.Clear();
                            SetParameter(param, obj, shopID);
                            cmd.ExecuteNonQuery();
                            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("insert sb_shop_bank shop_id/bank_id = {0}/{1}", shopID, obj.bankID), con);
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

        public void Update(long shopID, IList<EShopBank> objList)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();
                        string sql = "";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlParameterCollection param = cmd.Parameters;

                        foreach (EShopBank obj in objList)
                        {
                            param.Clear();
                            SetParameter(param, obj, shopID);
                            cmd.ExecuteNonQuery();
                            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("update sb_shop_bank shop_id/bank_id = {0}/{1}", shopID, obj.bankID), con);
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

        public void Delete(long shopID, EShopBank obj)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = "delete from sb_shop_bank where shop_id = @shop_id and bank_id = @bank_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    param.Clear();
                    base.AddSQLParam(param, "@shop_id", shopID);
                    base.AddSQLParam(param, "@bank_id", obj.bankID);

                    cmd.ExecuteNonQuery();
                    base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("delete sb_shop_bank shop_id/bank_id = {0}/{1}", shopID, obj.bankID), con);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void UpdateIsActive(long shopID, IList<EShopBank> objList)
        {
            try
            {
                using (SqlConnection con = base.CreateConnection())
                {
                    con.Open();
                    string sql = @"update sb_shop_bank 
                                      set active = @active
                                   where shop_id = @shop_id and bank_id = @bank_id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlParameterCollection param = cmd.Parameters;

                    foreach (EShopBank obj in objList)
                    {
                        param.Clear();
                        base.AddSQLParam(param, "@shop_id", shopID);
                        base.AddSQLParam(param, "@bank_id", obj.bankID);
                        cmd.ExecuteNonQuery();

                        base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                                         , string.Format("update active sb_shop_bank shop_id/bank_id = {0}/{1}", shopID, obj.bankID), con);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public IList<EShopBank> GetAllByShopID(long shopID)
        {
            try
            {
                string sql = @"select sb.*
	                                , b.bank_code
	                                , b.bank_name
	                                , b.image_url
                                from sb_shop_bank sb, sb_bank b
                                where sb.bank_id = b.bank_id
                                  and isnull(sb.active,'Y') = 'Y'
                                  and sb.shop_id = @shop_id
                                order by sb.created_date";
                DataTable dt = GetDataTable(sql, "@shop_id", shopID);
                List<EShopBank> objList = new List<EShopBank>();
                foreach (DataRow dto in dt.Rows)
                {
                    EShopBank obj = new EShopBank();
                    obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                    obj.bankID = BLUtil.NVLLong(dto["bank_id"]);
                    obj.bankCode = BLUtil.NVLString(dto["bank_code"]);
                    obj.bankName = BLUtil.NVLString(dto["bank_name"]);
                    obj.imageUrl = BLUtil.NVLString(dto["image_url"]);
                    obj.isMain = BLUtil.NVLString(dto["is_main"]);
                    obj.bookNo = BLUtil.NVLString(dto["book_no"]);
                    obj.bookName = BLUtil.NVLString(dto["book_name"]);
                    obj.branch = BLUtil.NVLString(dto["branch"]);
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

        private void SetParameter(SqlParameterCollection param, EShopBank obj, long shopID)
        {
            base.AddSQLParam(param, "@shop_id", shopID);
            base.AddSQLParam(param, "@bank_id", obj.bankID);
            base.AddSQLParam(param, "@book_name", obj.bookName);
            base.AddSQLParam(param, "@book_no", obj.bookNo);
            base.AddSQLParam(param, "@branch", obj.branch);
            base.AddSQLParam(param, "@is_main", obj.isMain);
            base.AddSQLParam(param, "@active", obj.active);
            base.AddCreateUpdate(param);
        }
    }
}
