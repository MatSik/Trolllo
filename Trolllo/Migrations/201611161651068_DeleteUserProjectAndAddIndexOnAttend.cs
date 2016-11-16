namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserProjectAndAddIndexOnAttend : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProjects", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProjects", "ProjectId", "dbo.Projects");
            DropIndex("dbo.AttendedToProjects", new[] { "Id" });
            DropIndex("dbo.AttendedToProjects", new[] { "ProjectId" });
            DropIndex("dbo.UserProjects", "UserAndProject");
            RenameColumn(table: "dbo.AttendedToProjects", name: "Id", newName: "ApplicationUserId");
            CreateIndex("dbo.AttendedToProjects", new[] { "ApplicationUserId", "ProjectId" }, unique: true, name: "UserAndProject");
            DropTable("dbo.UserProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        UserProjectId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserProjectId);
            
            DropIndex("dbo.AttendedToProjects", "UserAndProject");
            RenameColumn(table: "dbo.AttendedToProjects", name: "ApplicationUserId", newName: "Id");
            CreateIndex("dbo.UserProjects", new[] { "ApplicationUserId", "ProjectId" }, unique: true, name: "UserAndProject");
            CreateIndex("dbo.AttendedToProjects", "ProjectId");
            CreateIndex("dbo.AttendedToProjects", "Id");
            AddForeignKey("dbo.UserProjects", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.UserProjects", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
