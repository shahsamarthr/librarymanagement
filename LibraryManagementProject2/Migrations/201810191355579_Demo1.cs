namespace LibraryManagementProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        SerialNumber = c.String(nullable: false),
                        Author = c.String(),
                        Publisher = c.String(),
                        qty = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BorrowHistories",
                c => new
                    {
                        BorrowHistoryId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BorrowHistoryId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        Contact = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.BorrowHistories", "BookId", "dbo.Books");
            DropIndex("dbo.BorrowHistories", new[] { "UserId" });
            DropIndex("dbo.BorrowHistories", new[] { "BookId" });
            DropTable("dbo.Users");
            DropTable("dbo.BorrowHistories");
            DropTable("dbo.Books");
        }
    }
}
