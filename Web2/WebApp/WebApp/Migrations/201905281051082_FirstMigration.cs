namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ulica = c.String(),
                        Broj = c.String(),
                        Grad = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cenovniks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DO = c.DateTime(nullable: false),
                        OD = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CenovnikStavkas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDCenovnika = c.Int(nullable: false),
                        IDSt5avka = c.Int(nullable: false),
                        IDKoeficijent = c.Int(nullable: false),
                        Cena = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ODDatum = c.DateTime(nullable: false),
                        DoDatum = c.DateTime(nullable: false),
                        IDCenovnikStavka = c.Int(nullable: false),
                        IDKorisnik = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Koeficijents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Vrednost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Koordinates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Lozinka = c.String(),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Adresa = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                        TipKorisnika = c.Int(nullable: false),
                        Slika = c.String(),
                        Stanje = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.Int(nullable: false),
                        TipVoznje = c.Int(nullable: false),
                        Polasci = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RedVoznjes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Datum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        IDKoordinata = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Stavkas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipKarte = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Stavkas");
            DropTable("dbo.Stanicas");
            DropTable("dbo.RedVoznjes");
            DropTable("dbo.Linijas");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Koordinates");
            DropTable("dbo.Koeficijents");
            DropTable("dbo.Kartas");
            DropTable("dbo.CenovnikStavkas");
            DropTable("dbo.Cenovniks");
            DropTable("dbo.Adresas");
        }
    }
}
