using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Migrations;

namespace Nop.Data.CustomMigrations
{
    [NopMigration("2021/05/27 13:40:00:9037698", "Pdfs")]
    public class ProductCustomExtMigration006 : AutoReversingMigration
    {
        public override void Up()
        {
            if (this.Schema.Table(nameof(Product)).Column(nameof(Product.SpecificationFileName)).Exists())
            {
                Alter.Column(nameof(Product.SpecificationFileName))
                    .OnTable(nameof(Product))
                    .AsString(256)
                    .Nullable();

                Alter.Column(nameof(Product.AccessoriesFileName))
                    .OnTable(nameof(Product))
                    .AsString(256)
                    .Nullable();

            }
            else
            {
                Create.Column(nameof(Product.SpecificationFileName))
                    .OnTable(nameof(Product))
                    .AsString(256)
                    .Nullable();

                Create.Column(nameof(Product.AccessoriesFileName))
                    .OnTable(nameof(Product))
                    .AsString(256)
                    .Nullable();
            }
        }
    }
}
