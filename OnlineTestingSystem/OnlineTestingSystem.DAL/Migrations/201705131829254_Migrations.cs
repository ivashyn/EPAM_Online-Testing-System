namespace OnlineTestingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Certificates", "CertificateNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Certificates", "CertificateNumber", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
