namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addminiimages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "MiniImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "MiniImage");
        }
    }
}
