using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Catalog
{
    public partial class Product
    {
        public decimal? SupplierPrice { get; set; }
        public string SupplierPriceCurrency { get; set; }
        public string Supplier { get; set; }
        public DateTime? LastPriceRefresh { get; set; }
    }
}
