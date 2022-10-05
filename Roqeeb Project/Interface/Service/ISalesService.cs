using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ISalesService
    {
        Task<BaseResponse<SalesDTO>> Create(string cartId, string CustomerName, CancellationToken cancellationToken);
        Task<BaseResponse<IList<SalesDTO>>> GetAll(CancellationToken cancellationToken);
        Task<BaseResponse<IList<SalesDTO>>> GetByDate(DateTime date, CancellationToken cancellationToken);
        Task<BaseResponse<ViewSalesDTO>> ViewSalesToday(CancellationToken cancellationToken);
        Task<BaseResponse<ViewSalesDTO>> ViewSalesThisWeek(CancellationToken cancellationToken);
        Task<BaseResponse<ViewSalesDTO>> ViewSalesThisMonth(CancellationToken cancellationToken);
    }
}
