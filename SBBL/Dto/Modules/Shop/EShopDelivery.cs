using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Master;
using System.ComponentModel.DataAnnotations;

namespace SBBL.Dto.Modules.Shop
{
    public class EShopDelivery
    {
        public long shopID { get; set; }
        public long deliveryID { get; set; }
        public string deliveryCode { get; set; }
        public string deliveryName { get; set; }

        [Required(ErrorMessage = "กรุณาระบุ จำนวน >,=")]
        [Range(0, int.MaxValue, ErrorMessage = "กรุณาระบุตัวเลข")]
        public int? moreQty { get; set; }

        [Required(ErrorMessage = "กรุณาระบุ ราคา")]
        [Range(0, int.MaxValue, ErrorMessage = "กรุณาระบุตัวเลข")]
        public decimal? price { get; set; }

        public string isMain { get; set; }
        public string active { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

        public IList<EShopDelivery> ShopDeliveryList { get; set; }
        public IList<EDelivery> DeliveryList { get; set; }

    }
}
