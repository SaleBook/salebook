using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using SBBL.Dao;
using BL.Dao;
using SBBL.Dto.Modules.Master;
using System.Data;
using BL.Component.Common;
using SBBL.Component.Entity;
using SBBL.Component.Session;
using SBBL.Dto.Modules.Account;

namespace SBBL.Dao.Modules.Master
{
    public class AccountDao : BaseDAO<AccountDao>, IAccount
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region public method
        public void SetUserSeller(EFb obj, ref string msg)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long userID = 0;
                    string username = "";
                    string userFullName = "";
                    string roleCode = "SELLER";
                    string roleName = "";
                    long roleId = 0;
                    long shopID = 0;
                    using (SqlConnection con = base.CreateConnection())
                    {
                        con.Open();

                        // insert user not exists fbIb
                        EUser user = GetUserByFbID(obj.FbID);
                        if (user != null)
                        {
                            userID = user.userID;
                            username = user.userName;
                            userFullName = (user.firstName + " " + user.lastName).Trim();

                            ERole role = GetRoleByRoleCode(roleCode);
                            if (role != null)
                            {
                                roleId = role.roleID;
                                roleName = role.roleName;
                            }
                        }
                        else
                        {
                            userID = InsertUserByFb(obj, roleCode, con);
                            username = obj.FbName;
                            userFullName = obj.FbName;

                            ERole role = GetRoleByRoleCode(roleCode);
                            if (role != null)
                            {
                                roleId = role.roleID;
                            }
                        }

                        // set user id for login
                        Login.User.UserID = userID;
                        Login.User.UserName = username;
                        Login.User.UserFullName = userFullName;
                        Login.User.roleID = roleId;
                        Login.User.roleCode = roleCode;
                        Login.User.roleName = roleName;

                        // insert shop not exists fbIb
                        EShop shop = GetShopByFbID(obj.FanPage.FanPageID);
                        if (shop != null)
                        {
                            shopID = shop.shopID;
                        }
                        else
                        {
                            shopID = InsertShopByFb(obj, con);
                            InsertShopDocByFb(shopID, con);
                            InsertShopNoteByFb(shopID, con);
                            InsertShopOptionByFb(shopID, con);
                        }

                        // set shop id for login
                        Login.User.ShopID = shopID;
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserUserNotExistsFb(long shopID, string reletionCode, List<string> fbID)
        {
            throw new NotImplementedException();
        }

        public void InsertReletion(long shopID, long userID, string reletionCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateAddress(long userID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region private method
        private EUser GetUserByFbID(string fbID)
        {
            string sql = @"select *
                                , (select role_code from tbl_role a where a.role_id = u.role_id) as role_code
                                from tbl_user u
                                where u.fb_id = @fb_id";
            DataTable dt = GetDataTable(sql, "@fb_id", fbID);
            if (dt.Rows.Count > 0)
            {
                DataRow dto = dt.Rows[0];
                EUser obj = new EUser();
                obj.userID = BLUtil.NVLLong(dto["user_id"]);
                obj.roleID = BLUtil.NVLLong(dto["role_id"]);
                obj.roleCode = BLUtil.NVLString(dto["role_code"]);
                obj.fbID = BLUtil.NVLString(dto["fb_id"]);
                obj.userName = BLUtil.NVLString(dto["username"]);
                obj.password = BLUtil.NVLString(dto["password"]);
                obj.firstName = BLUtil.NVLString(dto["first_name"]);
                obj.lastName = BLUtil.NVLString(dto["last_name"]);
                obj.email = BLUtil.NVLString(dto["email"]);
                obj.tell = BLUtil.NVLString(dto["tell"]);
                obj.address = BLUtil.NVLString(dto["address"]);
                obj.province = BLUtil.NVLString(dto["province"]);
                obj.zipcode = BLUtil.NVLString(dto["zipcode"]);
                obj.active = BLUtil.NVLString(dto["active"]);
                obj.isDeleted = BLUtil.NVLString(dto["is_deleted"]);
                obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                return obj;
            }

            return null;
        }

        private EShop GetShopByFbID(string fbID)
        {
            string sql = @"select *
                                from sb_shop
                                where fb_id = @fb_id";
            DataTable dt = GetDataTable(sql, "@fb_id", fbID);
            if (dt.Rows.Count > 0)
            {
                DataRow dto = dt.Rows[0];
                EShop obj = new EShop();
                obj.shopID = BLUtil.NVLLong(dto["shop_id"]);
                obj.fbID = BLUtil.NVLString(dto["fb_id"]);
                obj.shopCode = BLUtil.NVLString(dto["shop_code"]);
                obj.shopName = BLUtil.NVLString(dto["shop_name"]);
                obj.active = BLUtil.NVLString(dto["active"]);
                obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                return obj;
            }

            return null;
        }

        private long InsertUserByFb(EFb obj, string roleCode, SqlConnection con)
        {
            string sql = @"declare @role_id bigint
                                    set @role_id = (select role_id from tbl_role where role_code = @role_code)

                                    insert tbl_user ( role_id, fb_id, username, first_name, active, is_deleted
				                                    , created_by, created_date, updated_by, updated_date)
		                                      values( @role_id, @fb_id, @username, @first_name, 'Y', 'N'
				                                    , @created_by, @created_date, @updated_by, @updated_date )";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameterCollection param = cmd.Parameters;

            param.Clear();

            base.AddSQLParam(param, "@role_code", roleCode);
            base.AddSQLParam(param, "@fb_id", obj.FbID);
            base.AddSQLParam(param, "@username", obj.FbName);
            base.AddSQLParam(param, "@first_name", obj.FbName);
            base.AddCreateUpdate(param);

            cmd.ExecuteNonQuery();
            long id = base.GetIdentity(con);
            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, obj.FbName
                         , string.Format("insert tbl_user user_id/fb_id = {0}/{1}", id, obj.FbID), con);

            return id;
        }

        private long InsertShopByFb(EFb obj, SqlConnection con)
        {
            string sql = @"insert sb_shop ( fb_id, shop_name, shop_code, active, is_deleted
				                        , created_by, created_date, updated_by, updated_date)
		                            values( @fb_id, @shop_name, @shop_code, 'Y', 'N'
				                        , @created_by, @created_date, @updated_by, @updated_date )";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameterCollection param = cmd.Parameters;

            param.Clear();

            base.AddSQLParam(param, "@fb_id", obj.FanPage.FanPageID);
            base.AddSQLParam(param, "@shop_name", obj.FanPage.FanPageName);
            base.AddSQLParam(param, "@shop_code", obj.FanPage.FanPageName);
            base.AddCreateUpdate(param);

            cmd.ExecuteNonQuery();
            long id = base.GetIdentity(con);
            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                         , string.Format("insert sb_shop shop_id/fb_id = {0}/{1}", id, obj.FanPage.FanPageID), con);

            return id;
        }

        private void InsertShopDocByFb(long shopId, SqlConnection con)
        {
            string sql = @"insert sb_shop_document ( shop_id, document_id, running, digit, active
						, created_by, created_date, updated_by, updated_date)

                        select @shop_id, document_id, 1, digit, 'Y', @created_by, @created_date, @updated_by, @updated_date
                        from TBL_DOCUMENT
                        where isnull(active, 'Y') = 'Y' and isnull(is_deleted, 'N') = 'N'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameterCollection param = cmd.Parameters;

            param.Clear();

            base.AddSQLParam(param, "@shop_id", shopId);
            base.AddCreateUpdate(param);

            cmd.ExecuteNonQuery();
            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                         , string.Format("insert sb_shop_document shop_id = {0}", shopId), con);
        }

        private void InsertShopNoteByFb(long shopId, SqlConnection con)
        {
            string sql = @"insert sb_shop_note ( shop_id, note_id, note, active
						, created_by, created_date, updated_by, updated_date)

                            select @shop_id, note_id, note, 'Y', @created_by, @created_date, @updated_by, @updated_date
                            from sb_note
                            where isnull(active, 'Y') = 'Y' and isnull(is_deleted, 'N') = 'N'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameterCollection param = cmd.Parameters;

            param.Clear();

            base.AddSQLParam(param, "@shop_id", shopId);
            base.AddCreateUpdate(param);

            cmd.ExecuteNonQuery();
            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                         , string.Format("insert sb_shop_note shop_id = {0}", shopId), con);
        }

        private void InsertShopOptionByFb(long shopId, SqlConnection con)
        {
            string sql = @"insert sb_shop_option (shop_id, option_id, start_date, finish_date, active
					 , created_by, created_date, updated_by, updated_date)
                    select @shop_id, option_id,  DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), dateadd(day, cast(option_value as int),  DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))), 'Y'
	                     , @created_by, @created_date, @updated_by, @updated_date
                    from sb_option
                    where option_group = 'SYSTEM'
                      and option_type = 'DAY'
                      and isnull(active, 'Y') = 'Y' 
                      and isnull(is_deleted, 'N') = 'N'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameterCollection param = cmd.Parameters;

            param.Clear();

            base.AddSQLParam(param, "@shop_id", shopId);
            base.AddCreateUpdate(param);

            cmd.ExecuteNonQuery();
            base.InsertLog(BLEnum.LogType.ACTIVITY, this.GetType().Name, Login.User.UserFullName
                         , string.Format("insert sb_shop_option shop_id = {0}", shopId), con);
        }

        private ERole GetRoleByRoleCode(string roleCode)
        {
            string sql = @"select *
                                from tbl_role
                                where role_code = @role_code";
            DataTable dt = GetDataTable(sql, "@role_code", roleCode);
            if (dt.Rows.Count > 0)
            {
                DataRow dto = dt.Rows[0];
                ERole obj = new ERole();
                obj.roleID = BLUtil.NVLLong(dto["role_id"]);
                obj.roleCode = BLUtil.NVLString(dto["role_code"]);
                obj.roleName = BLUtil.NVLString(dto["role_name"]);
                obj.active = BLUtil.NVLString(dto["active"]);
                obj.isDeleted = BLUtil.NVLString(dto["is_deleted"]);
                obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                return obj;
            }

            return null;
        }

        #endregion
    }
}
