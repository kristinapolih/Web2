namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stanicas", "X", c => c.Double(nullable: false));
            AddColumn("dbo.Stanicas", "Y", c => c.Double(nullable: false));
            DropColumn("dbo.Stanicas", "IDKoordinata");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stanicas", "IDKoordinata", c => c.Int(nullable: false));
            DropColumn("dbo.Stanicas", "Y");
            DropColumn("dbo.Stanicas", "X");
        }
    }
}
