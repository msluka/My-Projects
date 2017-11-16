namespace MVC_CRUD_AJAX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreatedFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileTitle = c.String(),
                        FileByte = c.Binary(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CreatedFiles");
        }
    }
}
