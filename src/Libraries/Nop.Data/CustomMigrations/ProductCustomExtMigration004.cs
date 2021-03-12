using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/05 09:00:00", "Supplier price: auto calculate")]
    public class ProductCustomExtMigration004 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Product.AutoCalculatePrice))
                .OnTable(nameof(Product))
                .AsBoolean();
        }
    }
}
