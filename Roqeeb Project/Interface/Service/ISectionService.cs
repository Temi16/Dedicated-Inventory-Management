using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.RequestModels.SectionRequestModels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ISectionService
    {
        Task<BaseResponse<SectionDTO>> CreateSection(CreateSectionRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<SectionDTO>> GetSection(string sectionId, CancellationToken cancellationToken);
        Task<BaseResponse<IList<SectionDTO>>> GetAllByStore(string StoreName, CancellationToken cancellationToken);
        Task<BaseResponse<SectionDTO>> GetByStore(string sectionName, string storeName, CancellationToken cancellationToken);
        
    }
}
