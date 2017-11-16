namespace SavePicture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCarClass : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Cars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
