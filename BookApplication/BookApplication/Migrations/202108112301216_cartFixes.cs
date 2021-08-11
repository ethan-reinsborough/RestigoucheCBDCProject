namespace BookApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartFixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "book_ID", "dbo.Books");
            DropIndex("dbo.Carts", new[] { "book_ID" });
            AddColumn("dbo.Carts", "bookID", c => c.Int(nullable: false));
            DropColumn("dbo.Carts", "book_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "book_ID", c => c.Int());
            DropColumn("dbo.Carts", "bookID");
            CreateIndex("dbo.Carts", "book_ID");
            AddForeignKey("dbo.Carts", "book_ID", "dbo.Books", "ID");
        }
    }
}
