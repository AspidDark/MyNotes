using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Rights;

namespace MyNotes.DataAccess.Mapper.Right
{
    public class LocalRightMap : BaseEntityMap<LocalRight>
    {
        public LocalRightMap() : base("local_right") { }

        public override void Configure(EntityTypeBuilder<LocalRight> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.AllowedUserId).HasColumnName("allowed_user_id").IsRequired();
            builder.Property(x => x.AccessType).HasColumnName("access_type").IsRequired();
            builder.Property(x => x.EntityType).HasColumnName("entity_type").IsRequired();
            builder.Property(x => x.EntityId).HasColumnName("entity_id").IsRequired();
        }
    }
}
