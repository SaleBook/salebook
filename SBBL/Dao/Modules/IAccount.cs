using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Account;
using BL.Component.Common;
using SBBL.Component.Entity;
using BL.Dto.Modules.SystemOperation;

namespace SBBL.Dao.Modules
{
    public interface IAccount
    {
        // common
        void SetUserSeller(EFb obj, ref string msg);
        void InserUserNotExistsFb(long shopID, string reletionCode, List<string> fbID);
        void InsertReletion(long shopID, long userID, string reletionCode);
        void UpdateAddress(long userID);
    }
}
