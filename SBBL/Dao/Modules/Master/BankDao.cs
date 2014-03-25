using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using SBBL.Dao;
using BL.Dao;
using SBBL.Dto.Modules.Master;

namespace SBBL.Dao.Modules.Master
{
    public class BankDao : BaseDAO<BankDao>, IMaster<EBank>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IList<EBank> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<EBank> GetIsActive()
        {
            throw new NotImplementedException();
        }

        public EBank GetByID(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IList<EBank> obj)
        {
            throw new NotImplementedException();
        }

        public void Update(IList<EBank> obj)
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
