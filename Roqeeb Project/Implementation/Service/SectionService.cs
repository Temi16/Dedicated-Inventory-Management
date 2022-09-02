using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels.SectionRequestModels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class SectionService : ISectionService
    {
        public Task<BaseResponse<SectionDTO>> CreateSection(CreateSectionRequestModel request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponse<SectionDTO>> GetSection(string sectionId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
