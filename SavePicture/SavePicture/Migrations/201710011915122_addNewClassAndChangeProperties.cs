namespace SavePicture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewClassAndChangeProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgTitle = c.String(),
                        ImgByte = c.Binary(),
                        ImgPath = c.String(),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
            DropColumn("dbo.Cars", "CarImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "CarImage", c => c.Binary());
            DropForeignKey("dbo.Images", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Images", new[] { "Car_Id" });
            DropTable("dbo.Images");
        }
    }
}
