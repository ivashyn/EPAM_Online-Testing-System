namespace OnlineTestingSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Certificates", "TestDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Certificates", "TestDate", c => c.DateTime(nullable: false));
        }
    }
}
