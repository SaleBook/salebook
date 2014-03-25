using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dto.Modules.Master
{
    public class EOption
    {
        public long optionID { get; set; }
        public string optionCode { get; set; }
        public string optionName { get; set; }
        public string note { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
