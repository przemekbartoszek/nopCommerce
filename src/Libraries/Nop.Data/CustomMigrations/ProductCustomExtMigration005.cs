using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/04/22 13:40:00:9037698", "WaitingForDelivery")]
    public class ProductCustomExtMigration005 : AutoReversingMigration
    {
        public override void Up()
        {
            if (this.Schema.Table(nameof(Product)).Column(nameof(Product.WaitingForDelivery)).Exists())
            {
                Alter.Column(nameof(Product.WaitingForDelivery))
                    .OnTable(nameof(Product))
                    .AsBoolean();

            }
            else
            {
                Create.Column(nameof(Product.WaitingForDelivery))
                    .OnTable(nameof(Product))
                    .AsBoolean();
            }
        }
    }
}
