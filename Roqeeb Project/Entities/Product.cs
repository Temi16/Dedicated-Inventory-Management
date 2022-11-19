using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Product : AuditableEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsAvalaible { get; set; }
        public int SetLowQuantity { get; set; }
        public string Image { get; set; }
        public ICollection<ProductSales> ProductSales { get; set; } = new HashSet<ProductSales>();
        public ICollection<ProductPurchase> ProductPurchase { get; set; } = new HashSet<ProductPurchase>();
        public ICollection<ProductSection> productSections { get; set; } = new HashSet<ProductSection>();

    }
}
