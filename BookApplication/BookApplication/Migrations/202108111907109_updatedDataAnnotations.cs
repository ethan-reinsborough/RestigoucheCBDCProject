namespace BookApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
