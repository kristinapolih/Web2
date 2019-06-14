namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventeenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stanicas", "Obrisana", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stanicas", "Stamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stanicas", "Stamp");
            DropColumn("dbo.Stanicas", "Obrisana");
        }
    }
}
