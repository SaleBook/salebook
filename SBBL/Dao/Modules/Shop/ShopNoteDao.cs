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

namespace SBBL.Dao.Modules.Shop
{
    public class ShopNoteDao : BaseDAO<ShopNoteDao>, IShopNote<EShopNote>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    }
}
