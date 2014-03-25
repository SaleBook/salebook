using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dto.Modules.Account
{
    public class EUser
    {
        public long userID { get; set; }
        public long roleID { get; set; }
        public string roleCode { get; set; }
        public string fbID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string tell { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string zipcode { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }

    public class ERole
    {
        public long roleID { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }

    public class EShop
    {
        public long shopID { get; set; }
        public string fbID { get; set; }
        public string shopName { get; set; }
        public string shopCode { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

        public IList<EShop> shopList { get; set; }
    }

}
