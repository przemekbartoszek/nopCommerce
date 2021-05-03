using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.DTO.Errors;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.DTOs.Products;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.ModelBinders;
using Nop.Plugin.Api.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Plugin.Api.Controllers
{
    public class ProductPricesController : BaseApiController
    {
        private readonly IProductApiService _productApiService;
        private readonly IProductService _productService;
        private readonly IDTOHelper _dtoHelper;

        public ProductPricesController(
            IJsonFieldsSerializer jsonFieldsSerializer, 
            IAclService aclService, 
            ICustomerService customerService, 
            IStoreMappingService storeMappingService, 
            IStoreService storeService, 
            IDiscountService discountService, 
            ICustomerActivityService customerActivityService, 
            ILocalizationService localizationService, 
            IPictureService pictureService,
            IProductApiService productApiService,
            IProductService productService, IDTOHelper dtoHelper) 
            : base(jsonFieldsSerializer, aclService, customerService, storeMappingService, storeService, discountService, customerActivityService, localizationService, pictureService)
        {
            this._productApiService = productApiService;
            _productService = productService;
            _dtoHelper = dtoHelper;
        }

        [HttpPut]
        [Route("/api/product_price/{id}")]
        [ProducesResponseType(typeof(ProductsRootObjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorsRootObject), 422)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateProduct([ModelBinder(typeof(JsonModelBinder<ProductPriceDto>))] Delta<ProductPriceDto> productDelta)
        {
            // Here we display the errors if the validation has failed at some point.
            if (!ModelState.IsValid)
            {
                return Error();
            }
            CustomerActivityService.InsertActivity("APIService", "Starting Product Update", null);

            var product = _productApiService.GetProductById(productDelta.Dto.Id);

            if (product == null)
            {
                return Error(HttpStatusCode.NotFound, "product", "not found");
            }

            product.NetPrice = productDelta.Dto.NetPrice;
            product.Price = productDelta.Dto.Price;
            product.SupplierPrice = productDelta.Dto.SupplierPrice;
            product.Supplier = productDelta.Dto.Supplier;
            product.SupplierPriceCurrency = productDelta.Dto.SupplierPriceCurrency;
            product.LastPriceRefresh = productDelta.Dto.LastPriceRefresh;
            product.Margin = productDelta.Dto.Margin;
            product.MaxPrice = productDelta.Dto.MaxPrice;
            product.AutoCalculatePrice = productDelta.Dto.AutoCalculatePrice;
            product.WaitingForDelivery = productDelta.Dto.WaitingForDelivery;

            product.UpdatedOnUtc = DateTime.UtcNow;
            _productService.UpdateProduct(product);

            CustomerActivityService.InsertActivity("APIService", $"Update product: '{product.Sku}' price: {productDelta.Dto.Price} ", product);
            var result = new ProductPricesRootObjectDto();
            result.Products.Add(productDelta.Dto);
            return new RawJsonActionResult(JsonFieldsSerializer.Serialize(result, string.Empty));


        }

        [HttpPost]
        [Route("/api/product_relations/{id}")]
        [ProducesResponseType(typeof(ProductsRootObjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorsRootObject), 422)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateProductRelations([ModelBinder(typeof(JsonModelBinder<ProductRelationsDto>))] Delta<ProductRelationsDto> productDelta)
        {
            // Here we display the errors if the validation has failed at some point.
            if (!ModelState.IsValid)
            {
                return Error();
            }
            CustomerActivityService.InsertActivity("APIService", "Starting Product Update", null);

            var product = _productApiService.GetProductById(productDelta.Dto.Id);

            if (product == null)
            {
                return Error(HttpStatusCode.NotFound, "product", "not found");
            }

            productDelta.Merge(product);


            UpdateRelatedProducts(product, productDelta.Dto.RelatedProductIds);
            UpdateCrossProducts(product, productDelta.Dto.CrossProductIds);

            CustomerActivityService.InsertActivity("APIService", LocalizationService.GetResource("ActivityLog.UpdateProduct"), product);

            // Preparing the result dto of the new product
            var productDto = _dtoHelper.PrepareProductDTO(product);

            var productsRootObject = new ProductsRootObjectDto();

            productsRootObject.Products.Add(productDto);

            var json = JsonFieldsSerializer.Serialize(productsRootObject, string.Empty);

            return new RawJsonActionResult(json);


        }

        private  void UpdateRelatedProducts(Product product, List<int> passedRelatedProductIds)
        {
            if (passedRelatedProductIds == null)
                return;

            var relatedProducts = _productService.GetRelatedProductsByProductId1(product.Id, showHidden: true);

            var noLongerRelatedProducts = relatedProducts.Where(p => !passedRelatedProductIds.Contains(p.ProductId2));

            // update all products that are no longer associated with our product
            foreach (var noLongerRelatedProduct in noLongerRelatedProducts)
            {
                _productService.DeleteRelatedProduct(noLongerRelatedProduct);
            }

            var newRelatedProducts = passedRelatedProductIds.Where(x => relatedProducts.All(r => r.ProductId2 != x));
            foreach (var newRelatedProductId in newRelatedProducts)
            {
                _productService.InsertRelatedProduct(new RelatedProduct
                {
                    DisplayOrder = 1,
                    ProductId1 = product.Id,
                    ProductId2 = newRelatedProductId
                });
            }
        }

        private void UpdateCrossProducts(Product product, List<int> passedCrossSellProductIds)
        {
            if (passedCrossSellProductIds == null)
                return;

            var crossSellProducts = _productService.GetCrossSellProductsByProductId1(product.Id, showHidden: true);

            var noLongerCrossSellProducts = crossSellProducts.Where(p => !passedCrossSellProductIds.Contains(p.ProductId2));

            // update all products that are no longer associated with our product
            foreach (var noLongerCrossSellProduct in noLongerCrossSellProducts)
            {
                _productService.DeleteCrossSellProduct(noLongerCrossSellProduct);
            }

            var newCrossSellProducts =
                passedCrossSellProductIds.Where(x => crossSellProducts.All(c => c.ProductId2 != x));
            foreach (var newCrossSellProductId in newCrossSellProducts)
            {
                _productService.InsertCrossSellProduct(new CrossSellProduct
                {
                    ProductId1 = product.Id,
                    ProductId2 = newCrossSellProductId
                });
            }
        }
    }
}
