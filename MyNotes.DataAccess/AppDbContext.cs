using Microsoft.EntityFrameworkCore;
using MyNotes.DataAccess.Mapper.Additional;
using MyNotes.DataAccess.Mapper.Core;
using MyNotes.DataAccess.Mapper.Right;
using MyNotes.Domain.Entities.Additional;
using MyNotes.Domain.Entities.Core;
using MyNotes.Domain.Entities.Rights;

namespace MyNotes.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("public");
            //Table name mapping
            modelBuilder.ApplyConfiguration(new TopicMap());
            modelBuilder.ApplyConfiguration(new ParagraphMap());
            modelBuilder.ApplyConfiguration(new NoteMap());
            modelBuilder.ApplyConfiguration(new FileEntityMap());

            modelBuilder.ApplyConfiguration(new CommentMap());

            modelBuilder.ApplyConfiguration(new GlobalRightMap());
            modelBuilder.ApplyConfiguration(new LocalRightMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<GlobalRight> GlobalRights { get; set; }
        public DbSet<LocalRight> LocalRights { get; set; }

    }
}
