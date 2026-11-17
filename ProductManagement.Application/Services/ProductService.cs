using AutoMapper;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.DTOs.Requests;
using ProductManagement.Application.DTOs.Responses;
using ProductManagement.Application.Entities;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        #region [-Fields-]
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        #endregion

        #region [-ctor-]
        public ProductService(IProductRepository productRepository, IMapper mapper = null)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        #endregion

        #region [-GetAll-]
        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }
        #endregion

        #region [-Get-]
        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return null;
            }

            return _mapper.Map<ProductResponse>(product);
        }
        #endregion

        #region [-Create-]
        public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);

            var createdProduct = await _productRepository.CreateAsync(product);

            return _mapper.Map<ProductResponse>(createdProduct);
        }
        #endregion

        #region [-Update-]
        public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            product.Id = id;
            var updatedProduct = await _productRepository.UpdateAsync(product);

            if (updatedProduct is null)
            {
                return null;
            }

            return _mapper.Map<ProductResponse>(updatedProduct);
        }
        #endregion

        #region [-Delete-]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }
        #endregion
    }
}
