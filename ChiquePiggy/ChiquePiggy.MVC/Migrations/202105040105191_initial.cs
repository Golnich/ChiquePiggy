namespace ChiquePiggy.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CHIQUEPIGGY",
                c => new
                    {
                        Id_Cliente = c.Int(nullable: false, identity: true),
                        Nome_Cliente = c.String(),
                        Saldo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Cliente);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CHIQUEPIGGY");
        }
    }
}
