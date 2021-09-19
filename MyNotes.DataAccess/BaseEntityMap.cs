using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Domain.Entities;

namespace MyNotes.DataAccess
{
    public abstract class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        private readonly string _tableName;
        public BaseEntityMap(string tableName)
        {
            _tableName = tableName;
        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(_tableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();

            builder.Property(x => x.OwnerId).HasColumnName("owner_id").IsRequired();

            builder.Property(x => x.CreateDate).HasColumnName("create_date").IsRequired();

            builder.Property(x => x.EditDate).HasColumnName("edit_date").IsRequired();

            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");

        }
    }
}
