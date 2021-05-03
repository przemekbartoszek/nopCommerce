using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.DTOs.Products
{
    [JsonObject(Title = "product_price")]
    public class ProductPriceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("net_price")]
        public decimal NetPrice { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("supplier_price")]
        public decimal? SupplierPrice { get; set; }

        [JsonProperty("supplier_price_currency")]
        public string SupplierPriceCurrency { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("last_price_refresh")]
        public DateTime? LastPriceRefresh { get; set; }

        [JsonProperty("margin")]
        public decimal? Margin { get; set; }

        [JsonProperty("max_price")]
        public decimal? MaxPrice { get; set; }

        [JsonProperty("auto_calculate_price")]
        public bool AutoCalculatePrice { get; set; }

        [JsonProperty("waiting_for_delivery")]
        public bool WaitingForDelivery { get; set; }
    }
}
