using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;

namespace Nop.Plugin.Api.DTOs.Products
{
    public class ProductFilesRootObjectDto : ISerializableObject
    {
        public ProductFilesRootObjectDto()
        {
            Products = new List<ProductFilesDto>();
        }

        [JsonProperty("products")]
        public IList<ProductFilesDto> Products { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "products";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(ProductFilesDto);
        }
    }
}
