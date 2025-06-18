
namespace Peristance.Data.Configrations
{
    //
    internal class DeliveryNethodConfigrations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(D=>D.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(D => D.ShortName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(D => D.Description)
                .HasColumnType("varchar")
                .HasMaxLength (100);

            builder.Property(D => D.DeliveryItem)
                .HasColumnType("varchar")
                .HasMaxLength(50);

        }
    }
}
