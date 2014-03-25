using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Dto.Modules.Master
{
    public class ENote
    {
        public long noteID { get; set; }
        public string noteCode { get; set; }
        public string noteName { get; set; }
        public string note { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
