namespace Roqeeb_Project.DTO_s
{
    public class PurchaseDTO
    {
        public string Id { get; set; }
        public AdminCartDTO cart { get; set; }
        public string SupplierName { get; set; }
        public string ReferenceNo { get; set; }
    }
    public class PendingPurchaseDTO
    {
        public AdminCartDTO cart { get; set; }
        public string ReferenceNo { get; set; }
        public string SupplierName { get; set; }
        public string DateCreated { get; set; }
    }
    public class CreatePurchaseRequestModel
    {
        public string CartId { get; set; }

    }
}
