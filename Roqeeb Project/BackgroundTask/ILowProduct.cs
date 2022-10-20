using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.BackgroundTask
{
    public interface ILowProduct
    {
        Task<BaseResponse<IList<LowProductDTO>>> LowProductMessage(Product product, CancellationToken cancellationToken);
    }
}
