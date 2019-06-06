namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EightMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kartas", "Obrisana", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kartas", "Obrisana");
        }
    }
}
