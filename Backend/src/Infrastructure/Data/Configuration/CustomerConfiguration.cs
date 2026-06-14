// using Domain.Customers;
// using Infrastructure.Idnetity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infrastructure.Data.Configuration;

// public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
// {
//     public void Configure(EntityTypeBuilder<Customer> builder)
//     {
//         builder.ToTable("Customers");

//         builder.HasKey(c => c.Id);
//         builder.Property(c => c.Id)
//         .ValueGeneratedNever();

//         builder.Property(c => c.Gender)
//         .HasConversion<string>();

//         builder.HasOne<AppUser>()
//         .WithOne()
//         .HasForeignKey<Customer>(c => c.UserId)
//         .OnDelete(DeleteBehavior.NoAction);

//         builder.HasQueryFilter(c => !c.IsDeleted);
//     }
// }