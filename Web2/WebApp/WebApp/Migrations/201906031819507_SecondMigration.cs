namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Linijas", "Naziv", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Linijas", "Naziv", c => c.Int(nullable: false));
        }
    }
}
