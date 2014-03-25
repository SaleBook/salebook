using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Dto.Modules.SystemOperation
{

    public class ERoleOperation
    {
        public long roleID { get; set; }
        public long operationID { get; set; }
    }

    public class EDocument
    {
        public long documentID { get; set; }
        public string documentCode { get; set; }
        public string documentName { get; set; }
        public string note { get; set; }
        public long running { get; set; }
        public int digit { get; set; }
        public string active { get; set; }
    }

    public class ESystemConfig
    {
        public long systemConfigID { get; set; }
        public string systemConfigCode { get; set; }
        public string systemConfigName { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string note { get; set; }
        public string active { get; set; }
    }

}
