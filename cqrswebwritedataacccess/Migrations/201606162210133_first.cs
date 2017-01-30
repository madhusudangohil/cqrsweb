namespace cqrswebwritedataacccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityKey = c.Guid(nullable: false),
                        EventKey = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        SystemType = c.String(),
                        EventData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventRecords");
        }
    }
}
