namespace Roqeeb_Project.View_Models.RequestModels
{
    public class CreateProductRequestModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public string SectionName { get; set; }
        public bool IsAvalaible { get; set; }
    }
}
