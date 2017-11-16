namespace SavePicture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "CarImage", c => c.Binary());
            DropColumn("dbo.Cars", "ImgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "ImgId", c => c.String());
            DropColumn("dbo.Cars", "CarImage");
        }
    }
}
