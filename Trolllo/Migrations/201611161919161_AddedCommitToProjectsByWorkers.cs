namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommitToProjectsByWorkers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AskForCommitToProjects",
                c => new
                    {
                        AskForCommitToProjectId = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AskForCommitToProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.WorkerId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => new { t.WorkerId, t.ProjectId }, unique: true, name: "CommitWorkerAndProject");
            
            CreateTable(
                "dbo.ProjectWorkers",
                c => new
                    {
                        ProjectWorkerId = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectWorkerId)
                .ForeignKey("dbo.AspNetUsers", t => t.WorkerId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => new { t.WorkerId, t.ProjectId }, unique: true, name: "AddedWorkerAndProject");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectWorkers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectWorkers", "WorkerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AskForCommitToProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.AskForCommitToProjects", "WorkerId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectWorkers", "AddedWorkerAndProject");
            DropIndex("dbo.AskForCommitToProjects", "CommitWorkerAndProject");
            DropTable("dbo.ProjectWorkers");
            DropTable("dbo.AskForCommitToProjects");
        }
    }
}
