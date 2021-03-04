using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.DTO.Errors;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.DTOs.Products;
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
            IProductService productService) 
            : base(jsonFieldsSerializer, aclService, customerService, storeMappingService, storeService, discountService, customerActivityService, localizationService, pictureService)
        {
            this._productApiService = productApiService;
            _productService = productService;
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

            product.Price = productDelta.Dto.Price;
            product.SupplierPrice = productDelta.Dto.SupplierPrice;
            product.Supplier = productDelta.Dto.Supplier;
            product.SupplierPriceCurrency = productDelta.Dto.SupplierPriceCurrency;
            product.LastPriceRefresh = productDelta.Dto.LastPriceRefresh;

            product.UpdatedOnUtc = DateTime.UtcNow;
            _productService.UpdateProduct(product);

            CustomerActivityService.InsertActivity("APIService", $"Update product: '{product.Sku}' price: {productDelta.Dto.Price} ", product);

            return new RawJsonActionResult(productDelta.Dto);


        }
    }
}
