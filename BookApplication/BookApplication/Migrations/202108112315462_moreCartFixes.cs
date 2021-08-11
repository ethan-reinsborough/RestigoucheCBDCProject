namespace BookApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreCartFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "quantity");
        }
    }
}
