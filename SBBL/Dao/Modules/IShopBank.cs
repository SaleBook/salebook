using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Account;
using BL.Component.Common;
using SBBL.Component.Entity;
using BL.Dto.Modules.SystemOperation;
using SBBL.Dto.Modules.Master;

namespace SBBL.Dao.Modules
{
    public interface IShopBank<T>
    {
        void Insert(long shopID, IList<T> objList);
        void Update(long shopID, IList<T> objList);
        void Delete(long shopID, T obj);

        void UpdateIsActive(long shopID, IList<T> objList);
        IList<T> GetAllByShopID(long shopID);

        T GetByID(long shopID, long bankID);
        IList<EBank> GetBankNotExists(long shopID);
    }
}
