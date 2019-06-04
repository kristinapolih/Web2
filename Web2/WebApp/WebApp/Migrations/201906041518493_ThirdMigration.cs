namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Linijas", "Datum", c => c.Int(nullable: false));
            DropTable("dbo.RedVoznjes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RedVoznjes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Datum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Linijas", "Datum");
        }
    }
}
