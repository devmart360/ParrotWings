namespace Devmart360.ParrotWings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatorUser_Id = c.Long(),
                        RecipientUser_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUser_Id)
                .ForeignKey("dbo.AbpUsers", t => t.RecipientUser_Id)
                .Index(t => t.CreatorUser_Id)
                .Index(t => t.RecipientUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "RecipientUser_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.Transactions", "CreatorUser_Id", "dbo.AbpUsers");
            DropIndex("dbo.Transactions", new[] { "RecipientUser_Id" });
            DropIndex("dbo.Transactions", new[] { "CreatorUser_Id" });
            DropTable("dbo.Transactions");
        }
    }
}
