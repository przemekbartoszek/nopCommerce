using System;
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

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.GrossPrice")]
        public decimal NetPrice { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.AutoCalculatePrice")]
        public bool AutoCalculatePrice { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.WaitingForDelivery")]
        public bool WaitingForDelivery { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.SpecificationFileName")]
        public string SpecificationFileName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.AccessoriesFileName")]
        public string AccessoriesFileName { get; set; }
    }
}
