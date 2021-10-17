using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Core
{
    public class NoteMap : BaseEntityMap<Note>
    {
        public NoteMap() : base("note") { }

        public override void Configure(EntityTypeBuilder<Note> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ParagraphId).HasColumnName("paragraph_id").IsRequired();
            builder.HasOne<Paragraph>(c => c.Paragraph).WithMany().HasForeignKey(x => x.ParagraphId);
            builder.Property(x => x.Message).HasColumnName("message");
            builder.Property(x => x.Name).HasColumnName("name");
        }
    }
}
