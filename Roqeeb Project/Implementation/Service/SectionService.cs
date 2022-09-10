using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels.SectionRequestModels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IStoreRepository _storeRepository;
        public SectionService(ISectionRepository sectionRepository, IStoreRepository storeRepository)
        {
            _sectionRepository = sectionRepository;
            _storeRepository = storeRepository;
        }
        public async Task<BaseResponse<SectionDTO>> CreateSection(CreateSectionRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var store = await _storeRepository.GetAsync(st => st.StoreName == request.StoreName, cancellationToken);
            if (store == null) return new BaseResponse<SectionDTO>
            {
                Message = "Store does not exist",
                Status = false
            };
            var section = await _sectionRepository.GetByStoreAsync(se => se.Store.StoreName == request.StoreName && se.SectionName == request.SectionName, cancellationToken);
            if (section != null) return new BaseResponse<SectionDTO>
            {
                Message = $"Section already exicts in {section.Store.StoreName}",
                Status = false
            };
            var newSection = new Section
            {
                SectionName = request.SectionName,
                SectionDescription = request.SectionDescription,
                StoreId = store.Id,
                IsDeleted = false
            };
            await _sectionRepository.CreateAsync(newSection, cancellationToken);
            return new BaseResponse<SectionDTO>
            {
                Data = new SectionDTO
                {
                    Id = newSection.Id,
                    SectionName = newSection.SectionName,
                    SectionDescription = newSection.SectionDescription
                },
                Message = "Created Successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<SectionDTO>>> GetAllByStore(string StoreName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sections = await _sectionRepository.GetAllByStoreAsync(se => se.Store.StoreName == StoreName, cancellationToken);
            if (sections == null) return new BaseResponse<IList<SectionDTO>>
            {
                Message = "Sections not found",
                Status = false
            };
            return new BaseResponse<IList<SectionDTO>>
            {
                Data = sections.Select(se => new SectionDTO
                {
                    Id = se.Id,
                    SectionName = se.SectionName,
                    SectionDescription = se.SectionDescription
                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<SectionDTO>> GetByStore(string sectionName, string storeName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var section = await _sectionRepository.GetByStoreAsync(se => se.Store.StoreName == storeName && se.SectionName == sectionName, cancellationToken);
            if (section == null) return new BaseResponse<SectionDTO>
            {
                Message = "cannot locate section",
                Status = false
            };
            return new BaseResponse<SectionDTO>
            {
                Data = new SectionDTO
                {
                    Id = section.Id,
                    SectionName = section.SectionName,
                    SectionDescription = section.SectionDescription
                },
                Message = "Successful",
                Status = true

            };

        }

        public Task<BaseResponse<SectionDTO>> GetSection(string sectionId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
