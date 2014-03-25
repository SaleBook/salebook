using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Dto.Modules.SystemOperation;
using SBBL.Dto.Modules.Account;

namespace BL.Dao.Modules
{
    public interface ISystemOperation
    {
        // User
        IList<EUser> GetUserAll();
        EUser GetUserByID(long userID);
        void InsertUser(IList<EUser> objList);
        void UpdateUser(IList<EUser> objList);
        void DeleteUser(IList<long> idList);

        // Role operation
        IList<ERoleOperation> GetRoleOperation(long roleID);
        void DeleteRoleOperation(long roleID);
        void insertRoleOperation(IList<ERoleOperation> objList);

        // Document
        IList<EDocument> GetDocumentAll();
        EDocument GetDocumentByID(long documentID);
        EDocument GetDocumentByCode(string documentCode);
        void UpdateDocument(IList<EDocument> objList);

        // System Config
        IList<ESystemConfig> GetSystemConfigAll();
        ESystemConfig GetSystemConfigByID(long documentID);
        ESystemConfig GetSystemConfigByCode(string documentCode, string category);
        void UpdateSystemConfig(IList<ESystemConfig> objList);

    }
}
