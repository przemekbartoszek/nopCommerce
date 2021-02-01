using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Api.DTO.Categories;
using Nop.Plugin.Api.DTO.Customers;
using Nop.Plugin.Api.DTO.Images;
using Nop.Plugin.Api.DTO.Languages;
using Nop.Plugin.Api.DTO.Manufacturers;
using Nop.Plugin.Api.DTO.OrderItems;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Api.DTO.ProductAttributes;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.DTO.ShoppingCarts;
using Nop.Plugin.Api.DTO.SpecificationAttributes;
using Nop.Plugin.Api.DTO.Stores;
using Nop.Plugin.Api.DTO.TaxCategory;
using Nop.Plugin.Api.DTOs.ProductReview;

namespace Nop.Plugin.Api.Helpers
{
    public interface IDTOHelper
    {
        Task<CustomerDto> PrepareCustomerDTO(Customer customer);
        Task<ProductDto> PrepareProductDTO(Product product);
        Task<CategoryDto> PrepareCategoryDTO(Category category);
        Task<OrderDto> PrepareOrderDTO(Order order);
        Task<ShoppingCartItemDto> PrepareShoppingCartItemDTO(ShoppingCartItem shoppingCartItem);
        Task<OrderItemDto> PrepareOrderItemDTO(OrderItem orderItem);
        Task<StoreDto> PrepareStoreDTO(Store store);
        Task<LanguageDto> PrepareLanguageDto(Language language);
        ProductAttributeDto PrepareProductAttributeDTO(ProductAttribute productAttribute);
        ProductSpecificationAttributeDto PrepareProductSpecificationAttributeDto(ProductSpecificationAttribute productSpecificationAttribute);
        SpecificationAttributeDto PrepareSpecificationAttributeDto(SpecificationAttribute specificationAttribute);
        Task<ManufacturerDto> PrepareManufacturerDto(Manufacturer manufacturer);
        Task<TaxCategoryDto> PrepareTaxCategoryDTO(TaxCategory taxCategory);
        Task<ImageMappingDto> PrepareProductPictureDTO(ProductPicture productPicture);
        Task<ProductReviewDto> PrepareProductReviewDTO(ProductReview productReview);
    }
}

