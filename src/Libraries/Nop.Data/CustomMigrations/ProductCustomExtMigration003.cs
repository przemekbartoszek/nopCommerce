using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/04 15:00:00", "Supplier price: net price")]
    public class ProductCustomExtMigration003 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Product.NetPrice))
                .OnTable(nameof(Product))
                .AsDecimal();
        }
    }
}
