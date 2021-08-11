namespace BookApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userID = c.String(),
                        book_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.book_ID)
                .Index(t => t.book_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "book_ID", "dbo.Books");
            DropIndex("dbo.Carts", new[] { "book_ID" });
            DropTable("dbo.Carts");
        }
    }
}
