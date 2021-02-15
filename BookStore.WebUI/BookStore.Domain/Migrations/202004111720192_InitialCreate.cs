namespace BookStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ISBN = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Specizailation = c.String(nullable: false),
                        Author = c.String(),
                        VersionDate = c.DateTime(nullable: false),
                        ImageData = c.Binary(nullable: false),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ISBN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
