﻿using Employees.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Infrastructure.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseIdentityColumn(1)
                .HasColumnOrder(0);

            builder.Property(e => e.Code)
                .IsRequired(true)
                .HasColumnOrder(1);

            builder.Property(e => e.FirstName)
                .IsRequired(true)
                .HasMaxLength(250)
                .HasColumnOrder(2);

            builder.Property(e => e.LastName)
                .IsRequired(true)
                .HasMaxLength(250)
                .HasColumnOrder(3);

            builder.Property(e => e.Email)
            .HasColumnOrder(4)
            .HasComputedColumnSql("[FirstName] + '.' + [LastName] + '@fakecompany.com'", stored: true);

            builder.Property(e => e.GenderAbbreviation)
                .HasColumnOrder(5)
                .IsRequired(true);

            builder.Property(e => e.Salary)
                .IsRequired(true)
                .HasColumnOrder(6)
                .HasColumnType("decimal(18,2)");
        }
    }
}