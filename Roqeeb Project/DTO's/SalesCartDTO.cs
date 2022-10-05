using System.Collections.Generic;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.DTO_s
{
    public class SalesCartDTO
    {
        public string Id { get; set; }
        public double TotalAmount { get; set; }
        public IList<ProductSalesCartDTO> ProductSalesCarts { get; set; }
    }
    public class AddSalesToCartRequestModel
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
    }
    public class ProductSalesCartDTO
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public double TotalPrice { get; set; }
    }
    public class EditProductSalesCart
    {
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
    }


}
