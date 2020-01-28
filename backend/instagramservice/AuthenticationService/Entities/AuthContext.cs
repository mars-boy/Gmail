using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Entities
{
    public class AuthContext : DbContext
    {
        #region Entities
        public DbSet<User> Users { get; set; }
        #endregion Entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique()
                .HasName("IDX_USR_USERNAME");

            modelBuilder.Entity<User>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique()
                .HasName("IDX_USR_Phone");
            #endregion User
        }

    }
}
