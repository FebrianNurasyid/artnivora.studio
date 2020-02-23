using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200206_2)]
    public class Migration_20200206_1AddMissingFields : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("VolunteerProfile").AlterColumn("MaritalStatus").AsInt32();            
        }

        public override void Down()
        {                        
            Alter.Table("VolunteerProfile").AlterColumn("MaritalStatus").AsString().Nullable();
        }
    }
}
