using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.DataAccess.Mapper.Core
{
    public class TopicMap : BaseEntityMap<Topic>
    {
        public TopicMap() : base("topic") { }

        public override void Configure(EntityTypeBuilder<Topic> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name).HasColumnName("name").IsRequired();
        }
    }
}
