using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dto.Modules.Master
{
    public class EBank
    {
        public long bankID { get; set; }
        public string bankCode { get; set; }
        public string bankName { get; set; }
        public string imageUrl { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
