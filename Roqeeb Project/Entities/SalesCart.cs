using System;
using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class SalesCart : AuditableEntity
    {
        public bool IsActive { get; set; } = true;
        public double TotalAmount { get; set; }
        public string Date { get; set; }
        public DateTime Week { get; set; }
        public string Month { get; set; }
        public IList<ProductSalesCart> ProductSalesCarts { get; set; }
    }
}
