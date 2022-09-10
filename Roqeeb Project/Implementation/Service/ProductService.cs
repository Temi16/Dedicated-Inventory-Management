using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels;
using Roqeeb_Project.View_Models.RequestModels.ProductRequestMosels;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IStoreRepository _storeRepository;
        public ProductService(IProductRepository productRepository, ISectionRepository sectionRepository, IStoreRepository storeRepository)
        {
            _productRepository = productRepository;
            _sectionRepository = sectionRepository;
            _storeRepository = storeRepository;
        }

        public async Task<BaseResponse<ProductDTO>> CreateProduct(CreateProductRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductAsync(p => p.ProductName == request.ProductName, cancellationToken);
            if (product != null) return new BaseResponse<ProductDTO>
            {
                Message = "Product already exists",
                Status = false
            };
            //var store = await _storeRepository.GetAllAsync(cancellationToken);
            //var storeId = store.Select(st => request.StoreId.Contains(st.Id)).SingleOrDefault();
            var section = await _sectionRepository.GetAsync(s => s.SectionName == request.SectionName, cancellationToken);
            if (section == null) return new BaseResponse<ProductDTO>
            {
                Message = "Section Name does not exists",
                Status = false
            };
            var newProduct = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                CostPrice = request.CostPrice,
                SellingPrice = request.SellingPrice,
                Quantity = request.Quantity,
                IsDeleted = false
            };
            await _productRepository.CreateProductAsync(newProduct, cancellationToken);
            await _productRepository.AddToSection(newProduct, section.SectionName, cancellationToken);
            return new BaseResponse<ProductDTO>
            {
                Data = new ProductDTO
                {
                    Id = newProduct.Id,
                    ProductName = newProduct.ProductName,
                    Quantity = newProduct.Quantity,
                    CostPrice = newProduct.CostPrice,
                    SellingPrice = newProduct.SellingPrice,
                    Description = newProduct.ProductDescription
                },
                Message = "Successfully Creaated",
                Status = true
            };
        }

        public async Task<BaseResponse<ProductDTO>> GetProduct(string productId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(productId)) throw new ArgumentNullException(nameof(productId));
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (product == null) return new BaseResponse<ProductDTO>
            {
                Message = "Product not found",
                Status = false
            };
            return new BaseResponse<ProductDTO>
            {
                Data = new ProductDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CostPrice = product.CostPrice,
                    SellingPrice = product.SellingPrice
                },
                Message = "Product Found",
                Status = true
            };


        }

        public async Task<BaseResponse<ProductDTO>> GetProductByName(string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(productName)) throw new ArgumentNullException(nameof(productName));
            var product = await _productRepository.GetProductByNameAsync(productName, cancellationToken);
            if (product == null) return new BaseResponse<ProductDTO>
            {
                Message = "Product not found",
                Status = false
            };
            return new BaseResponse<ProductDTO>
            {
                Data = new ProductDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CostPrice = product.CostPrice,
                    SellingPrice = product.SellingPrice
                },
                Message = "Product Found",
                Status = true
            };
        }

        public async Task<BaseResponse<TrackDTO>> TrackProduct(string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductAsync(p => p.ProductName == productName, cancellationToken);
            if (product == null) return new BaseResponse<TrackDTO>
            {
                Message = "Product not found",
                Status = false
            };
            var storeName = "";
            var newSection = "";
            foreach (var section in product.productSections)
            {
                newSection = section.Section.SectionName;
                storeName = section.Section.Store.StoreName;
            }
            return new BaseResponse<TrackDTO>
            {
                Data = new TrackDTO
                {
                    StoreName = storeName,
                    SectionName = newSection
                },
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<ProductDTO>> UpdateProduct(UpdateProductRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductAsync(p => p.ProductName == request.ProductName, cancellationToken);
            if (product == null) return new BaseResponse<ProductDTO>
            {
                Message = "Product Not Found",
                Status = false
            };
            product.Quantity = request.Quantity == 0 ? product.Quantity : request.Quantity;
            product.CostPrice = request.CostPrice == 0 ? product.CostPrice : request.CostPrice;
            product.SellingPrice = request.SellngPrice == 0 ? product.SellingPrice : request.SellngPrice;
            product.ProductName = request.ProductName ?? request.ProductName;
            await _productRepository.UpdateProduct(product, cancellationToken);
            return new BaseResponse<ProductDTO>
            {
                Data = new ProductDTO
                {
                    Id = product.Id,
                    Description = product.ProductDescription,
                    ProductName = product.ProductName,
                    CostPrice = product.CostPrice,
                    Quantity = product.Quantity,
                    SellingPrice = product.SellingPrice
                },
                Message = "Product Successfully Updated",
                Status = true

            };



        }

        public async Task<BaseResponse<IEnumerable<ProductDTO>>> ViewAllProducts(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var products = await _productRepository.ViewProductsAsync(cancellationToken);
            if (products == null) return new BaseResponse<IEnumerable<ProductDTO>>
            {
                Message = "No products available",
                Status = false
            };
            return new BaseResponse<IEnumerable<ProductDTO>>
            {
                Data = products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Description = p.ProductDescription,
                    ProductName = p.ProductName,
                    CostPrice = p.CostPrice,
                    Quantity = p.Quantity,
                    SellingPrice = p.SellingPrice
                }).ToList(),
                Message = "Successfull",
                Status = true
            };


        }
    }
}
