namespace HTLegal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefeedback2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.E_Home_FeedbackClient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icon_108x108 = c.String(maxLength: 500),
                        Content = c.String(),
                        Author = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.E_Home_FeedbackClient");
        }
    }
}
