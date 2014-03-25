using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using SBBL.Dao;

namespace BL.Dao.Modules.SystemOperation
{
    public class UserDao : BaseDAO<UserDao>, ISystemOperation
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IList<SBBL.Dto.Modules.Account.EUser> GetUserAll()
        {
            throw new NotImplementedException();
        }

        public SBBL.Dto.Modules.Account.EUser GetUserByID(long userID)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(IList<SBBL.Dto.Modules.Account.EUser> objList)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(IList<SBBL.Dto.Modules.Account.EUser> objList)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(IList<long> idList)
        {
            throw new NotImplementedException();
        }

        public IList<Dto.Modules.SystemOperation.ERoleOperation> GetRoleOperation(long roleID)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoleOperation(long roleID)
        {
            throw new NotImplementedException();
        }

        public void insertRoleOperation(IList<Dto.Modules.SystemOperation.ERoleOperation> objList)
        {
            throw new NotImplementedException();
        }

        public IList<Dto.Modules.SystemOperation.EDocument> GetDocumentAll()
        {
            throw new NotImplementedException();
        }

        public Dto.Modules.SystemOperation.EDocument GetDocumentByID(long documentID)
        {
            throw new NotImplementedException();
        }

        public Dto.Modules.SystemOperation.EDocument GetDocumentByCode(string documentCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(IList<Dto.Modules.SystemOperation.EDocument> objList)
        {
            throw new NotImplementedException();
        }

        public IList<Dto.Modules.SystemOperation.ESystemConfig> GetSystemConfigAll()
        {
            throw new NotImplementedException();
        }

        public Dto.Modules.SystemOperation.ESystemConfig GetSystemConfigByID(long documentID)
        {
            throw new NotImplementedException();
        }

        public Dto.Modules.SystemOperation.ESystemConfig GetSystemConfigByCode(string documentCode, string category)
        {
            throw new NotImplementedException();
        }

        public void UpdateSystemConfig(IList<Dto.Modules.SystemOperation.ESystemConfig> objList)
        {
            throw new NotImplementedException();
        }
    }
}
