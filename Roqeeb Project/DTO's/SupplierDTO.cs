using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class SupplierDTO
    {
        public string Id { get; set; }
        public string SupplierName { get; set; }
        public IList<PurchaseDTO> Purchases { get; set; }
        
    }

    public class CreateSupplierRequestModel
    {
        public string SupplierName { get; set; }
    }
}
