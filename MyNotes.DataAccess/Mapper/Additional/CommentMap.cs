using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Additional;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Additional
{
    public class CommentMap : BaseEntityMap<Comment>
    {
        public CommentMap() : base("comment") { }

        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.ParagraphId).HasColumnName("paragraph_id").IsRequired();
            builder.HasOne<Paragraph>(c => c.Paragraph).WithMany().HasForeignKey(x => x.ParagraphId);
            builder.Property(x => x.Message).HasColumnName("message");
        }
    }
}
