namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtheenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Linijas", "Obrisana", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Linijas", "Obrisana");
        }
    }
}
