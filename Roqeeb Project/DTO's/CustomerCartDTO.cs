using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
  
        public class CustomerCartDTO
        {
            public string Id { get; set; }
            public double TotalAmount { get; set; }
            public string Date { get; set; }
            public IList<ProductCustomerCartDTO> ProductCustomerCarts { get; set; }
        }
        public class AddOrdersToCartRequestModel
        {
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }
        }
        public class ProductCustomerCartDTO
        {
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }
            public double ProductPrice { get; set; }
            public double TotalPrice { get; set; }
        }
}
