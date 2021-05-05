using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Base;

namespace Nop.Plugin.Api.DTOs.Products
{
    [JsonObject(Title = "product_relations")]
    public class ProductRelationsDto : BaseDto
    {
        [JsonProperty("related_productIds")]
        public List<int> RelatedProductIds { get; set; }

        [JsonProperty("cross_productIds")]
        public List<int> CrossProductIds { get; set; }

        [JsonProperty("associated_productIds")]
        public List<int> AssociatedProductIds { get; set; }

    }
}
