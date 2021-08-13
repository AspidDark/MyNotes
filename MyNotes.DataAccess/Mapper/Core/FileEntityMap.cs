using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Core
{
    public class FileEntityMap : BaseEntityMap<FileEntity>
    {
        public FileEntityMap() : base("file_entity") { }

        public override void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ParagraphId).HasColumnName("paragraph_id").IsRequired();
            builder.HasOne<Paragraph>(c => c.Paragraph).WithMany().HasForeignKey(x => x.ParagraphId);
            builder.Property(x => x.Path).HasColumnName("path");
        }
    }
}
