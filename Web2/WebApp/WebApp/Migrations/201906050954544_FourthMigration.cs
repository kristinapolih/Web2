namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Koeficijents", "Djak", c => c.Single(nullable: false));
            AddColumn("dbo.Koeficijents", "Pensioner", c => c.Single(nullable: false));
            DropColumn("dbo.Koeficijents", "Ime");
            DropColumn("dbo.Koeficijents", "Vrednost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Koeficijents", "Vrednost", c => c.Single(nullable: false));
            AddColumn("dbo.Koeficijents", "Ime", c => c.String());
            DropColumn("dbo.Koeficijents", "Pensioner");
            DropColumn("dbo.Koeficijents", "Djak");
        }
    }
}
