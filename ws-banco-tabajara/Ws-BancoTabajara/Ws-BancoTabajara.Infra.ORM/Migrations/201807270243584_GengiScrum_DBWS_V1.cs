namespace Ws_BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    public partial class GengiScrum_DBWS_V1 : DbMigration
    {
        [ExcludeFromCodeCoverage]
        public override void Up()
        {
            CreateTable(
                "dbo.TBBankAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                        Activated = c.Boolean(nullable: false),
                        Limit = c.Double(nullable: false),
                        TotalBalance = c.Double(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBClient", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.TBClient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        CPF = c.String(nullable: false, maxLength: 14),
                        RG = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        OperationType = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        BankAccount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBBankAccount", t => t.BankAccount_Id, cascadeDelete: true)
                .Index(t => t.BankAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBTransaction", "BankAccount_Id", "dbo.TBBankAccount");
            DropForeignKey("dbo.TBBankAccount", "Client_Id", "dbo.TBClient");
            DropIndex("dbo.TBTransaction", new[] { "BankAccount_Id" });
            DropIndex("dbo.TBBankAccount", new[] { "Client_Id" });
            DropTable("dbo.TBTransaction");
            DropTable("dbo.TBClient");
            DropTable("dbo.TBBankAccount");
        }
    }
}
