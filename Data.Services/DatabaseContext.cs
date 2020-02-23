namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using static Artnivora.Studio.Portal.Data.Services.Helpers.DatabaseConfigurationHelper;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> optionsBuilder) : base(optionsBuilder)
        {

        }

        public DatabaseContext()
        {

        }

		public DbSet<User> User { get; set; }
		public DbSet<Address> Address { get; set; }
        public DbSet<ParticipantProfile> ParticipantProfile { get; set; }
        public DbSet<VolunteerProfile> VolunteerProfile { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        //Messages
        public DbSet<Message> Message { get; set; }
        public DbSet<MailBox> MailBox { get; set; }
        public DbSet<MessageAttachments> MessageAttachments { get; set; }
        public DbSet<MessageAttachment> MessageAttachment { get; set; }
        public DbSet<Recipients> Recipients { get; set; }

        public DbSet<Production> Production { get; set; }
        public DbSet<ProductionAttachment> ProductionAttachment { get; set; }
        public DbSet<ProductionAttachments> ProductionAttachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var t = System.IO.Directory.GetCurrentDirectory();
                var appSettings = System.IO.File.ReadAllText(@"appsettings.json");
                var app = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(appSettings);
                optionsBuilder.UseSqlServer(app.ConnectionStrings.Database);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductionAttachments>()
              .HasOne(ma => ma.ProductionAttachment)
              .WithMany(mas => mas.Attachments)
              .HasForeignKey(p => p.ProductionAttachementId)
              .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<Production>()
                .HasMany(m => m.ProductionAttachments)
                .WithOne(a => a.Production)
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(a => a.ProductionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
