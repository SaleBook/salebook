using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dao.Modules
{
    public interface IMaster<T>
    {
        // common
        IList<T> GetAll();
        IList<T> GetIsActive();
        T GetByID(long id);

        void Insert(IList<T> obj);
        void Update(IList<T> obj);
        void Delete(Guid id);

        // validate
        bool IsHaveCode(string code);
    }
}
