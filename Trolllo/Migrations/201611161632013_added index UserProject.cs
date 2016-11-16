namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedindexUserProject : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserProjects", new[] { "ApplicationUserId" });
            DropIndex("dbo.UserProjects", new[] { "ProjectId" });
            CreateIndex("dbo.UserProjects", new[] { "ApplicationUserId", "ProjectId" }, unique: true, name: "UserAndProject");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProjects", "UserAndProject");
            CreateIndex("dbo.UserProjects", "ProjectId");
            CreateIndex("dbo.UserProjects", "ApplicationUserId");
        }
    }
}
