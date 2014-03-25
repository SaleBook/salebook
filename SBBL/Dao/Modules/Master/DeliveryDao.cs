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

namespace SBBL.Dao.Modules.Master
{
    public class DeliveryDao : BaseDAO<DeliveryDao>, IMaster<EDelivery>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IList<EDelivery> GetAll()
        {
            try
            {
                string sql = @"select *
                                from sb_delivery
                                where isnull(is_deleted,'N') = 'N'
                                order by delivery_id";
                DataTable dt = GetDataTable(sql);
                List<EDelivery> objList = new List<EDelivery>();
                foreach (DataRow dto in dt.Rows)
                {
                    EDelivery obj = new EDelivery();
                    obj.deliveryID = BLUtil.NVLLong(dto["delivery_id"]);
                    obj.deliveryCode = BLUtil.NVLString(dto["delivery_code"]);
                    obj.deliveryName = BLUtil.NVLString(dto["delivery_name"]);
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

        public IList<EDelivery> GetIsActive()
        {
            try
            {
                string sql = @"select *
                                from sb_delivery
                                where isnull(active,'Y') = 'Y'
                                  and isnull(is_deleted,'N') = 'N'
                                order by delivery_id";
                DataTable dt = GetDataTable(sql);
                List<EDelivery> objList = new List<EDelivery>();
                foreach (DataRow dto in dt.Rows)
                {
                    EDelivery obj = new EDelivery();
                    obj.deliveryID = BLUtil.NVLLong(dto["delivery_id"]);
                    obj.deliveryCode = BLUtil.NVLString(dto["delivery_code"]);
                    obj.deliveryName = BLUtil.NVLString(dto["delivery_name"]);
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

        public EDelivery GetByID(long id)
        {
            try
            {
                string sql = @"select *
                                from sb_delivery
                                where delivery_id = @delivery_id";
                DataTable dt = GetDataTable(sql, "@delivery_id", id);
                EDelivery obj = new EDelivery();
                if (dt.Rows.Count > 0)
                {
                    DataRow dto = dt.Rows[0];
                    obj.deliveryID = BLUtil.NVLLong(dto["deliveryID"]);
                    obj.deliveryCode = BLUtil.NVLString(dto["deliveryCode"]);
                    obj.deliveryName = BLUtil.NVLString(dto["deliveryName"]);
                    obj.active = BLUtil.NVLString(dto["active"]);
                    obj.createdBy = BLUtil.NVLString(dto["created_by"]);
                    obj.createdDate = BLUtil.NVLDate(dto["created_date"]);
                    obj.updatedBy = BLUtil.NVLString(dto["updated_by"]);
                    obj.updatedDate = BLUtil.NVLDate(dto["updated_date"]);
                }

                return obj;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void Insert(IList<EDelivery> obj)
        {
            throw new NotImplementedException();
        }

        public void Update(IList<EDelivery> obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool IsHaveCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
