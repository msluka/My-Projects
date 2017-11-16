namespace SavePicture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "ImgId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "ImgId");
        }
    }
}
