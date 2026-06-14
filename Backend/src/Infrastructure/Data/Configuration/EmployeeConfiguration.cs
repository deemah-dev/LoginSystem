// using Domain.Employees;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infrastructure.Data.Configuration;

// public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
// {
//     public void Configure(EntityTypeBuilder<Employee> builder)
//     {
//         builder.ToTable("Employees");

//         builder.HasKey(e => e.Id);
//         builder.Property(e => e.Id)
//         .ValueGeneratedNever();

//         builder.Property(e => e.Position)
//         .HasConversion<string>();

//         builder.Property(e => e.Department)
//         .HasConversion<string>();

//         builder.Property(e => e.Gender)
//         .HasConversion<string>();
//     }
// }