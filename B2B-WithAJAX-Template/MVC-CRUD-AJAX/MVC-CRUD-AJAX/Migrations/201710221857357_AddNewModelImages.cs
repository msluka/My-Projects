namespace MVC_CRUD_AJAX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewModelImages : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Images");
        }
    }
}
