using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<BaseResponse<SupplierDTO>> CreateSupplier(CreateSupplierRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.SupplierName = request.SupplierName.ToLower();
            var supplier = await _supplierRepository.GetAsync(s => s.SupplierName.ToLower() == request.SupplierName, cancellationToken);
            if (supplier != null) return new BaseResponse<SupplierDTO>
            {
                Message = "Supplier already Exists",
                Status = false
            };
            var mySupplier = new Supplier
            {
                SupplierName = request.SupplierName,
            };
            await _supplierRepository.CreateAsync(supplier, cancellationToken);
            return new BaseResponse<SupplierDTO>
            {
                Data = new SupplierDTO
                {
                    Id = mySupplier.Id,
                    SupplierName = mySupplier.SupplierName,
                    Purchases = mySupplier.Purchases.Select(p => new PurchaseDTO
                    {
                        Id = p.Id,
                        cart = new AdminCartDTO
                        {
                            Id = p.AdminCartId,
                            Products = p.AdminCart.productCarts.Select(pr => new ProductCartDTO
                            {
                                ProductName = pr.ProductName,
                                Quantity = pr.Quantity
                            }).ToList(),
                        }
                    }).ToList()

                },
                Message = "Successfully Created",
                Status = true
            };


        }

        public async Task<BaseResponse<IList<SupplierDTO>>> GetAllSuppliers(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var suppliers = await _supplierRepository.GetAllAsync(cancellationToken);
            if (suppliers.Count == 0) return new BaseResponse<IList<SupplierDTO>>
            {
                Message = "No Supplier found",
                Status = false
            };
            return new BaseResponse<IList<SupplierDTO>>
            {
                
                Data = suppliers.Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName,
                    Purchases = s.Purchases.Select(p => new PurchaseDTO
                    {
                        Id = p.Id,
                        cart = new AdminCartDTO
                        {
                            Id = p.AdminCartId,
                            Products = p.AdminCart.productCarts.Select(pr => new ProductCartDTO
                            {
                                ProductName = pr.ProductName,
                                Quantity = pr.Quantity
                            }).ToList(),
                        }
                    }).ToList()

                }).ToList(),
                Message = "Successful",
                Status = true

            };
        }
    }
}
