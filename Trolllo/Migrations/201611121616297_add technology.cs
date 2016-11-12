namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtechnology : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TechnologyId);
            
            AddColumn("dbo.Projects", "TechnologyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "TechnologyId");
            AddForeignKey("dbo.Projects", "TechnologyId", "dbo.Technologies", "TechnologyId", cascadeDelete: true);
            DropColumn("dbo.Projects", "Technology");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Technology", c => c.String());
            DropForeignKey("dbo.Projects", "TechnologyId", "dbo.Technologies");
            DropIndex("dbo.Projects", new[] { "TechnologyId" });
            DropColumn("dbo.Projects", "TechnologyId");
            DropTable("dbo.Technologies");
        }
    }
}
