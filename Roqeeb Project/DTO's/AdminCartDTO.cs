using System.Collections.Generic;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.DTO_s
{
    public class AdminCartDTO
    {
        public string Id { get; set; }
        public double TotalAmount { get; set; }
        public IList<ProductDTO> products { get; set; }
    }
    public class CreateCartRequestModel
    {
        public string PurchaseId { get; set; }
        
    }
}
