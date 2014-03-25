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
    public interface IProduct<T>
    {
        void InsertBySyncFB(long shopID, string albumFbId, string albumName, IList<string> urlList, IList<string> nameList, IList<string> noteList, int count);
        
        void Insert(long shopID, IList<T> objList);
        void Update(long shopID, IList<T> objList);
        void Delete(long shopID, T obj);

        void UpdateIsActive(long shopID, IList<long> productID);
        IList<T> GetAllByShopID(long shopID);

        T GetByID(long shopID,long productID);
        bool IsUniqe(long shopID, string productCode);
    }
}
