namespace HTLegal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udateCOnfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E_WebsiteConfiguration", "MetaDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.E_WebsiteConfiguration", "MetaDescription");
        }
    }
}
