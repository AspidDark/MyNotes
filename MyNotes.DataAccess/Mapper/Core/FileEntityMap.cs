using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Core
{
    public class FileEntityMap : BaseEntityMap<FileEntity>
    {
        public FileEntityMap() : base("fileEntity_map") { }

        public override void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.NoteId).HasColumnName("note_id").IsRequired();
            builder.HasOne<Note>(c => c.Note).WithMany().HasForeignKey(x => x.NoteId);
            builder.Property(x => x.Path).HasColumnName("path");
        }
    }
}
