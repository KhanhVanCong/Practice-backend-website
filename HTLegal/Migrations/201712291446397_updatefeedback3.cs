namespace HTLegal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefeedback3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E_Home_FeedbackClient", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.E_Home_FeedbackClient", "Order");
        }
    }
}
