using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/04 12:00:00", "Supplier price: margin")]
    public class ProductCustomExtMigration002 : AutoReversingMigration
    {
        public override void Up()
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
