using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Catalog
{
    public partial class Product
    {
        public decimal NetPrice { get; set; }
        public decimal? SupplierPrice { get; set; }
        public string SupplierPriceCurrency { get; set; }
        public string Supplier { get; set; }
        public DateTime? LastPriceRefresh { get; set; }
        public decimal? Margin { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool AutoCalculatePrice { get; set; }
        public bool WaitingForDelivery { get; set; }
        public string SpecificationFileName { get; set; }
        public string AccessoriesFileName { get; set; }
    }
}
