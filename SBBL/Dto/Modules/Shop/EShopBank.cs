using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Master;
using System.ComponentModel.DataAnnotations;
namespace SBBL.Dto.Modules.Shop
{
    public class EShopBank
    {
        public long shopID { get; set; }
        public long bankID { get; set; }
        public string bankCode { get; set; }
        public string bankName { get; set; }
        public string imageUrl { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ ชื่อ-นามสกุล")]
        public string bookName { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ Book No.")]
        public string bookNo { get; set; }
        public string branch { get; set; }
        public string isMain { get; set; }
        public string active { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

        public IList<EShopBank> shopBankList { get; set; }
        public IList<EBank> bankList { get; set; }
    }

}
