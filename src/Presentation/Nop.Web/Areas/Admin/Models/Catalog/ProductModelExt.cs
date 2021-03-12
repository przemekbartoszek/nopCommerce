using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    public partial class ProductModel
    {
        [NopResourceDisplayName("Admin.Catalog.Products.Fields.SupplierPrice")]
        public decimal? SupplierPrice { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.SupplierPriceCurrency")]
        public string SupplierPriceCurrency { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Supplier")]
        public string Supplier { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.LastPriceRefresh")]
        public DateTime? LastPriceRefresh { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Margin")]
        public decimal? Margin { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.MaxPrice")]
        public decimal? MaxPrice { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.NetPrice")]
        public decimal NetPrice { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.AutoCalculatePrice")]
        public bool AutoCalculatePrice { get; set; }
    }
}
