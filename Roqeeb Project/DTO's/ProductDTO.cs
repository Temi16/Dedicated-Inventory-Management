using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public int SetLowQuantity { get; set; }
        public IList<SectionDTO> SectionName { get; set; }
    }
}
