﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Features;

namespace Selu383.SP24.Api.Data;

public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, 
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DataContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var userRoleBuilder = builder.Entity<UserRole>();

        userRoleBuilder.HasKey(x => new { x.UserId, x.RoleId });

        userRoleBuilder.HasOne(x => x.Role).
            WithMany(x => x.Users).
            HasForeignKey(x => x.RoleId);

        userRoleBuilder.HasOne(x => x.User)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.UserId);

        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

    }
}