using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200222_1)]
    public class Migration_20200222_AddProductionAttachments : DatabaseMigration
    {
        public override void Up()
        {
            Create.Table("ProductionAttachment")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FilePath").AsString()
                .WithColumn("FileName").AsString()
                .WithColumn("ContentType").AsString();

            Create.Table("ProductionAttachments")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ProductionId").AsGuid()
                .WithColumn("ProductionAttachementId").AsGuid();
        }
        public override void Down()
        {
            Delete.Table("ProductionAttachment");
            Delete.Table("ProductionAttachments");            
        }
    }

}
