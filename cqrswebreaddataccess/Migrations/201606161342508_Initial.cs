namespace cqrswebreaddataccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Televisions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        FromChannel = c.Int(nullable: false),
                        ToChannel = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Televisions");
        }
    }
}
