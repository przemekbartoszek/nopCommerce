using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Api.DTOs.Products
{
    public class ProductPriceDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public decimal? SupplierPrice { get; set; }
        public string SupplierPriceCurrency { get; set; }
        public string Supplier { get; set; }
        public DateTime? LastPriceRefresh { get; set; }
    }
}
