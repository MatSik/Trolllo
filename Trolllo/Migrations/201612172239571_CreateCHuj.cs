namespace Trolllo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCHuj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chujs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Place = c.String(),
                        Type = c.String(),
                        Wykonawca = c.String(),
                        Architekt = c.String(),
                        TypeOfBuilding = c.String(),
                        Style = c.String(),
                        Metraz = c.String(),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chujs");
        }
    }
}
