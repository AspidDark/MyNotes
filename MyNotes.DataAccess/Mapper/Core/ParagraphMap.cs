using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Core
{
    public class ParagraphMap : BaseEntityMap<Paragraph>
    {
        public ParagraphMap() : base("paragraph") { }

        public override void Configure(EntityTypeBuilder<Paragraph> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name).HasColumnName("name").IsRequired();

            builder.Property(x => x.TopicId).HasColumnName("topic_id").IsRequired();
            builder.HasOne<Topic>(c => c.Topic).WithMany().HasForeignKey(x => x.TopicId);
            builder.Property(x => x.Message).HasColumnName("message").HasMaxLength(5000);
        }
    }
}
