namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korisniks", "IDUser", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korisniks", "IDUser");
        }
    }
}
