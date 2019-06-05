namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kartas", "Cena", c => c.Single(nullable: false));
            AddColumn("dbo.Kartas", "TipKarte", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kartas", "TipKarte");
            DropColumn("dbo.Kartas", "Cena");
        }
    }
}
