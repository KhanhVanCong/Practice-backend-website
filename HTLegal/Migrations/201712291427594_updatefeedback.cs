namespace HTLegal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E_Home_Content", "Img_1920x329_Feedback", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.E_Home_Content", "Img_1920x329_Feedback");
        }
    }
}
