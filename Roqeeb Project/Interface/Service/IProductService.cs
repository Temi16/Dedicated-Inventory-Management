using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.RequestModels;
using Roqeeb_Project.View_Models.RequestModels.ProductRequestMosels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IProductService
    {
        Task<BaseResponse<ProductDTO>> GetProduct(string productId, CancellationToken cancellationToken);
        Task<BaseResponse<ProductDTO>> CreateProduct(CreateProductRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<ProductDTO>> UpdateProduct(UpdateProductRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<ProductDTO>> GetProductByName(string productName, CancellationToken cancellationToken);
        Task<BaseResponse<IEnumerable<ProductDTO>>> ViewAllProducts(CancellationToken cancellationToken);
        Task<BaseResponse<TrackDTO>> TrackProduct(string productName, CancellationToken cancellationToken);
        Task<BaseResponse<ProductDTO>> DeleteProduct(string productName, CancellationToken cancellationToken);

    }
}
