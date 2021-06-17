using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/03/05 09:00:00:9037698", "Supplier price: auto calculate")]
    public class ProductCustomExtMigration004 : AutoReversingMigration
    {
        public override void Up()
        {
            if (this.Schema.Table(nameof(Product)).Column(nameof(Product.AutoCalculatePrice)).Exists())
            {
                Alter.Column(nameof(Product.AutoCalculatePrice))
                    .OnTable(nameof(Product))
                    .AsBoolean();
            }
            else
            {
                Create.Column(nameof(Product.AutoCalculatePrice))
                    .OnTable(nameof(Product))
                    .AsBoolean();
            }
        }
    }
}
