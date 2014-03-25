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
    public class NoteDao : BaseDAO<NoteDao>, IMaster<ENote>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IList<ENote> GetAll()
        {
            try
            {
                string sql = @"select *
                                from sb_note
                                where isnull(is_deleted,'N') = 'N'
                                order by note_id";
                DataTable dt = GetDataTable(sql);
                List<ENote> objList = new List<ENote>();
                foreach (DataRow dto in dt.Rows)
                {
                    ENote obj = new ENote();
                    obj.noteID = BLUtil.NVLLong(dto["noteID"]);
                    obj.noteCode = BLUtil.NVLString(dto["note_code"]);
                    obj.noteName = BLUtil.NVLString(dto["note_name"]);
                    obj.note = BLUtil.NVLString(dto["note"]);
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

        public IList<ENote> GetIsActive()
        {
            try
            {
                string sql = @"select *
                                from sb_note
                                where isnull(active,'Y') = 'Y'
                                  and isnull(is_deleted,'N') = 'N'
                                order by note_id";
                DataTable dt = GetDataTable(sql);
                List<ENote> objList = new List<ENote>();
                foreach (DataRow dto in dt.Rows)
                {
                    ENote obj = new ENote();
                    obj.noteID = BLUtil.NVLLong(dto["noteID"]);
                    obj.noteCode = BLUtil.NVLString(dto["note_code"]);
                    obj.noteName = BLUtil.NVLString(dto["note_name"]);
                    obj.note = BLUtil.NVLString(dto["note"]);
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

        public ENote GetByID(long id)
        {
            try
            {
                string sql = @"select *
                                from sb_note
                                where note_id = @note_id";
                DataTable dt = GetDataTable(sql, "@note_id", id);
                ENote obj = new ENote();
                if (dt.Rows.Count > 0)
                {
                    DataRow dto = dt.Rows[0];
                    obj.noteID = BLUtil.NVLLong(dto["noteID"]);
                    obj.noteCode = BLUtil.NVLString(dto["note_code"]);
                    obj.noteName = BLUtil.NVLString(dto["note_name"]);
                    obj.note = BLUtil.NVLString(dto["note"]);
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

        public void Insert(IList<ENote> obj)
        {
            throw new NotImplementedException();
        }

        public void Update(IList<ENote> obj)
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
