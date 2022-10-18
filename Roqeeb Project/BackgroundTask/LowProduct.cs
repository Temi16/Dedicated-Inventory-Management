using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.BackgroundTask
{
    public class LowProduct : ILowProduct
    {
        private readonly IProductRepository _productRepository;
        public LowProduct(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<LowProductDTO>> LowProductMessage(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var myProduct = await _productRepository.GetProductByIdAsync(product.Id, cancellationToken);
            if (myProduct == null) return new BaseResponse<LowProductDTO>
            {
                Message = "Failed",
                Status = false
            };
            return new BaseResponse<LowProductDTO>
            {
                Data = new LowProductDTO
                {
                    ProductName = myProduct.ProductName,
                    ProductQuantity = myProduct.Quantity
                },
                Message = "Successfull",
                Status = true
            };
        }
    }
}
