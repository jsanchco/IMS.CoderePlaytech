namespace IMS.CoderePlaytech.Infrastructure
{
    #region Using

    using Domain.Entities;
    using IMS.CoderePlaytech.Infrastructure.Helpers;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;

    #endregion

    public class EFContextSQL : DbContext
    {
        #region Members

        public virtual DbSet<Barcode> Barcodes { get; set; }
        public virtual DbSet<BarcodeType> BarcodeTypes { get; set; }
        public virtual DbSet<BarcodeState> BarcodeStates { get; set; }

        public static long InstanceCount;

        #endregion

        public EFContextSQL(DbContextOptions options) : base(options) => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
