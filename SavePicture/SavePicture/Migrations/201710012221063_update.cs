namespace SavePicture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Images", new[] { "Car_Id" });
            DropColumn("dbo.Images", "Car_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Car_Id", c => c.Int());
            CreateIndex("dbo.Images", "Car_Id");
            AddForeignKey("dbo.Images", "Car_Id", "dbo.Cars", "Id");
        }
    }
}
