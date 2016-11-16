namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendToProjectEntityandSuccesonProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendedToProjects",
                c => new
                    {
                        AttendToProjectId = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttendToProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "StateOfProject", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendedToProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.AttendedToProjects", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.AttendedToProjects", new[] { "ProjectId" });
            DropIndex("dbo.AttendedToProjects", new[] { "Id" });
            DropColumn("dbo.Projects", "StateOfProject");
            DropTable("dbo.AttendedToProjects");
        }
    }
}
