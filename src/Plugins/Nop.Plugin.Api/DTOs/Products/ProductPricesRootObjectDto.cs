using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs.Products;

namespace Nop.Plugin.Api.DTO.Products
{
    public class ProductPricesRootObjectDto : ISerializableObject
    {
        public ProductPricesRootObjectDto()
        {
            Products = new List<ProductPriceDto>();
        }

        [JsonProperty("products")]
        public IList<ProductPriceDto> Products { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "products";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (ProductPriceDto);
        }
    }
}