namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwelwethMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stanicas", "IDAdresa", c => c.Int(nullable: false));
            DropColumn("dbo.Stanicas", "Adresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stanicas", "Adresa", c => c.String());
            DropColumn("dbo.Stanicas", "IDAdresa");
        }
    }
}
