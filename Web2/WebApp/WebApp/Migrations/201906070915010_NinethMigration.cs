namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinethMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinijaStanicas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDLinija = c.Int(nullable: false),
                        IDStanica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LinijaStanicas");
        }
    }
}
