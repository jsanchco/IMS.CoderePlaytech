namespace IMS.CoderePlaytech.Infrastructure.Helpers
{
    #region Using

    using IMS.CoderePlaytech.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

    #endregion

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarcodeType>().HasData(
                new BarcodeType { Id = 1, Name = "Deposit" },
                new BarcodeType { Id = 2, Name = "Withdrawal" }
            );

            modelBuilder.Entity<BarcodeState>().HasData(
                new BarcodeState { Id = 1, Description = "Waiting" },
                new BarcodeState { Id = 2, Description = "Used" },
                new BarcodeState { Id = 3, Description = "Expired" }
            );
        }
    }
}
