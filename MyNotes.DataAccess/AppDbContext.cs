using Microsoft.EntityFrameworkCore;
using MyNotes.DataAccess.Mapper.Additional;
using MyNotes.DataAccess.Mapper.Core;

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
            modelBuilder.ApplyConfiguration(new CommentsMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
