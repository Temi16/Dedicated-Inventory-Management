using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels.StoreRequestModels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<BaseResponse<StoreDTO>> Create(CreateStoreRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var store = await _storeRepository.GetAsync(st => st.StoreName == request.StoreName, cancellationToken);
            if (store != null) return new BaseResponse<StoreDTO>
            {
                Message = "store already exists",
                Status = false
            };
            var newStore = new Store
            {
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                IsDeleted = false
            };
            var create = await _storeRepository.CreateAsync(newStore, cancellationToken);
            return new BaseResponse<StoreDTO>
            {
                Data = new StoreDTO
                {
                    Id = newStore.Id,
                    StoreName = newStore.StoreName,
                    StoreDescription = newStore.StoreDescription
                },
                Message = "Created successfully",
                Status = true
            };


        }
    }
}
