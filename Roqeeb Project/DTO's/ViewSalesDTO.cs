using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class ViewSalesDTO
    {
        public Dictionary<string, int> Products { get; set; }
        public double TotalCostPrice { get; set; }
        public double TotalSellingPrice { get; set; }
        public double Profit { get; set; }
    }
}
