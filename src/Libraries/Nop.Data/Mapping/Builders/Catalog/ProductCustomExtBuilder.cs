﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.Mapping.Builders.Catalog
{
    [NopMigration("2021/03/03 16:00:00", "Supplier price")]
    public class ProductCustomExtBuilder : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Product.Supplier))
                .OnTable(nameof(Product))
                .AsString(255)
                .Nullable();

            Create.Column(nameof(Product.SupplierPrice))
                .OnTable(nameof(Product))
                .AsDecimal()
                .Nullable();

            Create.Column(nameof(Product.SupplierPriceCurrency))
                .OnTable(nameof(Product))
                .AsString(3)
                .Nullable();

            Create.Column(nameof(Product.LastPriceRefresh))
                .OnTable(nameof(Product))
                .AsDateTime()
                .Nullable();
        }
    }
}
