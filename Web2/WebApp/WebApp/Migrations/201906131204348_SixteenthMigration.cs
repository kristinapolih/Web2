namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixteenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cenovniks", "Stamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Linijas", "Stamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Linijas", "Stamp");
            DropColumn("dbo.Cenovniks", "Stamp");
        }
    }
}
