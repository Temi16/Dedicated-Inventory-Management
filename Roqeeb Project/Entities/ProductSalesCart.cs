using System;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductSalesCart : AuditableEntity
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public DateTime Week { get; set; }
        public string SalesCartId { get; set; }
        public SalesCart SalesCart { get; set; }
    }
}
