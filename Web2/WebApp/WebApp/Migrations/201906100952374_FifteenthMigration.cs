namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifteenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinijaStanicas", "BrojStanice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LinijaStanicas", "BrojStanice");
        }
    }
}
