namespace Devmart360.ParrotWings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "CreatorUser_Id", newName: "CreatorUserId");
            RenameIndex(table: "dbo.Transactions", name: "IX_CreatorUser_Id", newName: "IX_CreatorUserId");
            AddColumn("dbo.Transactions", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Transactions", "LastModifierUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "LastModifierUserId");
            DropColumn("dbo.Transactions", "LastModificationTime");
            RenameIndex(table: "dbo.Transactions", name: "IX_CreatorUserId", newName: "IX_CreatorUser_Id");
            RenameColumn(table: "dbo.Transactions", name: "CreatorUserId", newName: "CreatorUser_Id");
        }
    }
}
