using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/04 12:00:00:9037698", "Supplier price: margin")]
    public class ProductCustomExtMigration002 : AutoReversingMigration
    {
        public override void Up()
        {
            if (this.Schema.Table(nameof(Product)).Column(nameof(Product.Margin)).Exists())
            {
                Alter.Column(nameof(Product.Margin))
                    .OnTable(nameof(Product))
                    .AsDecimal()
                    .Nullable();

                Alter.Column(nameof(Product.MaxPrice))
                    .OnTable(nameof(Product))
                    .AsDecimal()
                    .Nullable();
            }
            else
            {
                Create.Column(nameof(Product.Margin))
                    .OnTable(nameof(Product))
                    .AsDecimal()
                    .Nullable();

                Create.Column(nameof(Product.MaxPrice))
                    .OnTable(nameof(Product))
                    .AsDecimal()
                    .Nullable();
            }
        }
    }
}
