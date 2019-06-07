namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirteenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Linijas", "Broj", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Linijas", "Broj");
        }
    }
}
