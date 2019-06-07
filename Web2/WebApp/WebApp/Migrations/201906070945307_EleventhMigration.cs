namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EleventhMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stanicas", "IsStanica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stanicas", "IsStanica");
        }
    }
}
