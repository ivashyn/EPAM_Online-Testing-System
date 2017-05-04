namespace OnlineTestingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sertificates", newName: "Certificates");
            AddColumn("dbo.Certificates", "CertificateNumber", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Certificates", "SertificateNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Certificates", "SertificateNumber", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Certificates", "CertificateNumber");
            RenameTable(name: "dbo.Certificates", newName: "Sertificates");
        }
    }
}
