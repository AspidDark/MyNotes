using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities.Rights;

namespace MyNotes.DataAccess.Mapper.Right
{
    public class GlobalRightMap : BaseEntityMap<GlobalRight>
    {
        public GlobalRightMap() : base("global_right") { }

        public override void Configure(EntityTypeBuilder<GlobalRight> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.AccessType).HasColumnName("access_type").IsRequired();
        }
    }
}
