namespace Ws_BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfoasfkodasofefaew : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TBTransaction", name: "BankAccount_Id", newName: "BankAccountId");
            RenameIndex(table: "dbo.TBTransaction", name: "IX_BankAccount_Id", newName: "IX_BankAccountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TBTransaction", name: "IX_BankAccountId", newName: "IX_BankAccount_Id");
            RenameColumn(table: "dbo.TBTransaction", name: "BankAccountId", newName: "BankAccount_Id");
        }
    }
}
