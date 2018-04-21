namespace HTLegal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HTLegalContext : DbContext
    {
        public HTLegalContext()
            : base("name=HTLegalContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<E_AccessFunctionInPage> E_AccessFunctionInPage { get; set; }
        public virtual DbSet<E_AccessFunctionRole> E_AccessFunctionRole { get; set; }
        public virtual DbSet<E_AccessPage> E_AccessPage { get; set; }
        public virtual DbSet<E_Attorneys> E_Attorneys { get; set; }
        public virtual DbSet<E_Blog> E_Blog { get; set; }
        public virtual DbSet<E_Blog_Categories> E_Blog_Categories { get; set; }
        public virtual DbSet<E_Blog_Reply> E_Blog_Reply { get; set; }
        public virtual DbSet<E_Blog_Reply_Reply> E_Blog_Reply_Reply { get; set; }
        public virtual DbSet<E_Client> E_Client { get; set; }
        public virtual DbSet<E_CMS_AboutUs> E_CMS_AboutUs { get; set; }
        public virtual DbSet<E_CMS_Attorney> E_CMS_Attorney { get; set; }
        public virtual DbSet<E_CMS_Blog> E_CMS_Blog { get; set; }
        public virtual DbSet<E_CMS_ConTactUs> E_CMS_ConTactUs { get; set; }
        public virtual DbSet<E_CMS_Home> E_CMS_Home { get; set; }
        public virtual DbSet<E_CMS_Services> E_CMS_Services { get; set; }
        public virtual DbSet<E_DocumentCenter> E_DocumentCenter { get; set; }
        public virtual DbSet<E_Feature_Services> E_Feature_Services { get; set; }
        public virtual DbSet<E_Home_Content> E_Home_Content { get; set; }
        public virtual DbSet<E_Home_Slide> E_Home_Slide { get; set; }
        public virtual DbSet<E_PractiveAreas> E_PractiveAreas { get; set; }
        public virtual DbSet<E_Roles> E_Roles { get; set; }
        public virtual DbSet<E_Services> E_Services { get; set; }
        public virtual DbSet<E_UploadMoreFiles> E_UploadMoreFiles { get; set; }
        public virtual DbSet<E_Users> E_Users { get; set; }
        public virtual DbSet<E_ViewerMonitor> E_ViewerMonitor { get; set; }
        public virtual DbSet<E_WebsiteConfiguration> E_WebsiteConfiguration { get; set; }
        public virtual DbSet<E_Home_FeedbackClient> E_Home_FeedbackClient { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_AccessFunctionInPage>()
                .Property(e => e.FunctionCode)
                .IsUnicode(false);

            modelBuilder.Entity<E_AccessFunctionInPage>()
                .Property(e => e.PageCode)
                .IsUnicode(false);

            modelBuilder.Entity<E_AccessFunctionInPage>()
                .HasMany(e => e.E_AccessFunctionRole)
                .WithRequired(e => e.E_AccessFunctionInPage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<E_AccessFunctionRole>()
                .Property(e => e.FunctionCode)
                .IsUnicode(false);

            modelBuilder.Entity<E_AccessPage>()
                .Property(e => e.PageCode)
                .IsUnicode(false);

            modelBuilder.Entity<E_AccessPage>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<E_AccessPage>()
                .HasMany(e => e.E_AccessFunctionInPage)
                .WithRequired(e => e.E_AccessPage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.Picture_270x288)
                .IsUnicode(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.Picture_470x638)
                .IsUnicode(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.ProfessionalExperiences_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.Twifter)
                .IsUnicode(false);

            modelBuilder.Entity<E_Attorneys>()
                .Property(e => e.Google)
                .IsUnicode(false);

            modelBuilder.Entity<E_Blog>()
                .Property(e => e.Title_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_Blog>()
                .Property(e => e.Content_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_Blog>()
                .Property(e => e.Picture_810x305)
                .IsUnicode(false);

            modelBuilder.Entity<E_Blog>()
                .HasMany(e => e.E_Blog_Reply)
                .WithOptional(e => e.E_Blog)
                .HasForeignKey(e => e.BlogId);

            modelBuilder.Entity<E_Blog_Categories>()
                .Property(e => e.Name_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_Blog_Categories>()
                .HasMany(e => e.E_Blog)
                .WithOptional(e => e.E_Blog_Categories)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<E_Blog_Reply>()
                .HasMany(e => e.E_Blog_Reply_Reply)
                .WithOptional(e => e.E_Blog_Reply)
                .HasForeignKey(e => e.ReplyId);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Organization)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Hotline)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Introduce_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Logo_1920x400)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Picture_Left)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_AboutUs>()
                .Property(e => e.Picture_Right)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Attorney>()
                .Property(e => e.Title_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Attorney>()
                .Property(e => e.Comment_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.Logo_1920x940)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.Title_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.SubTitle_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.Logo_1920x940_Slide)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.Title_Slide_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.SubTitle_Slide_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.LegalConsultantPanelTitle_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.LegalConsultantPanelWorkTime_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_CMS_Home>()
                .Property(e => e.FreeConsultantBottonPanel_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_DocumentCenter>()
                .Property(e => e.Title_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_DocumentCenter>()
                .Property(e => e.Description_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_PractiveAreas>()
                .Property(e => e.Name_En)
                .IsUnicode(false);

            modelBuilder.Entity<E_Roles>()
                .HasMany(e => e.E_AccessFunctionRole)
                .WithOptional(e => e.E_Roles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<E_UploadMoreFiles>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<E_UploadMoreFiles>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<E_Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<E_Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<E_Users>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<E_Users>()
                .Property(e => e.LangCode)
                .IsUnicode(false);

            modelBuilder.Entity<E_Users>()
                .HasOptional(e => e.E_Attorneys)
                .WithRequired(e => e.E_Users);

            modelBuilder.Entity<E_WebsiteConfiguration>()
                .Property(e => e.NotificationEmail)
                .IsUnicode(false);

            modelBuilder.Entity<E_WebsiteConfiguration>()
                .Property(e => e.SupportEmail)
                .IsUnicode(false);

            modelBuilder.Entity<E_WebsiteConfiguration>()
                .Property(e => e.SupportEmailPassword)
                .IsUnicode(false);
        }
    }
}
