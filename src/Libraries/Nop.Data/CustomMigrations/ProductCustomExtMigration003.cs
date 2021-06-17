using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/04 15:00:00:9037698", "Supplier price: net price")]
    public class ProductCustomExtMigration003 : AutoReversingMigration
    {
        public override void Up()
        {
            if (this.Schema.Table(nameof(Product)).Column(nameof(Product.GrossPrice)).Exists())
            {
                Alter.Column(nameof(Product.GrossPrice))
                    .OnTable(nameof(Product))
                    .AsDecimal();
            }
            else
            {
                Create.Column(nameof(Product.GrossPrice))
                    .OnTable(nameof(Product))
                    .AsDecimal();
            }
        }
    }
}
