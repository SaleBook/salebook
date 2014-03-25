using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Master;
using System.ComponentModel.DataAnnotations;
namespace SBBL.Dto.Modules.Shop
{
    public class EProduct
    {
        public long shopID { get; set; }
        public long productID { get; set; }
        public string album { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ รหัสสินค้า")]
        public string productCode { get; set; }
        public string productName { get; set; }
        public string note { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ ราคา")]
        public decimal price { get; set; }
        public decimal promotionPrice { get; set; }
        public int qty { get; set; }
        public string isQty { get; set; }
        public string isPreOrder { get; set; }
        public string imageUrl { get; set; }
        public string active { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

        public IList<EProduct> productList { get; set; }
    }

}
