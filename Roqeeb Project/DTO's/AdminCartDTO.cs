using System;
using System.Collections.Generic;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.DTO_s
{
    public class AdminCartDTO
    {
        public string Id { get; set; }
        public double TotalAmount { get; set; }
        public IList<ProductCartDTO> Products { get; set; }
    }
    public class ProductCartDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    public class AddToCartRequestModel
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
       
        
    }
}
