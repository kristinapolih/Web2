namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Korisniks", "IDUser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Korisniks", "IDUser", c => c.Int(nullable: false));
        }
    }
}
