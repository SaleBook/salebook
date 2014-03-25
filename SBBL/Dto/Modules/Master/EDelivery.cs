using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dto.Modules.Master
{
    public class EDelivery
    {
        public long deliveryID { get; set; }
        public string deliveryCode { get; set; }
        public string deliveryName { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
