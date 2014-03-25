using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Component.Entity
{
    public class ELogin
    {
        public string UserIP { get; set; }
        public long UserID { get; set; }
        public long ShopID { get; set; }
        public long roleID { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public DateTime Login { get; set; }
        public EFb objFb { get; set; }
    }
}
