namespace BookApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basicBuild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "ID", "dbo.Covers");
            DropForeignKey("dbo.Books", "Id", "dbo.Covers");
            DropIndex("dbo.Books", new[] { "Id" });
            AddColumn("dbo.Files", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Files", "ApprovedBy", c => c.String());
            AddForeignKey("dbo.Files", "ID", "dbo.Books", "ID", cascadeDelete: true);
            DropTable("dbo.Covers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        imageSelected = c.String(nullable: false, maxLength: 150),
                        printDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Files", "ID", "dbo.Books");
            DropColumn("dbo.Files", "ApprovedBy");
            DropColumn("dbo.Files", "Approved");
            CreateIndex("dbo.Books", "Id");
            AddForeignKey("dbo.Books", "Id", "dbo.Covers", "ID");
            AddForeignKey("dbo.Files", "ID", "dbo.Covers", "ID", cascadeDelete: true);
        }
    }
}
