namespace TPDMS.RestApi.Models
{
    using System.Data.Entity;

    public partial class TPDMSDbModel : DbContext
    {
        public TPDMSDbModel()
            : base("name=TPDMSDbModel")
        {
        }

        public virtual DbSet<admUsers> admUsers { get; set; }
        public virtual DbSet<admUsersRoles> admUsersRoles { get; set; }
        public virtual DbSet<DataTypes> DataTypes { get; set; }
        public virtual DbSet<Entities> Entities { get; set; }
        public virtual DbSet<EntityFields> EntityFields { get; set; }
        public virtual DbSet<EntityKeys> EntityKeys { get; set; }
        public virtual DbSet<MappingFields> MappingFields { get; set; }
        public virtual DbSet<Mappings> Mappings { get; set; }
        public virtual DbSet<C42Order> C42Order { get; set; }
        public virtual DbSet<C42Order_aUIs> C42Order_aUIs { get; set; }
        public virtual DbSet<C42Order_upUIs> C42Order_upUIs { get; set; }
        public virtual DbSet<appDomains> appDomains { get; set; }
        public virtual DbSet<EconomicOperators> EconomicOperators { get; set; }
        public virtual DbSet<EOFacilities> EOFacilities { get; set; }
        public virtual DbSet<EOIDs> EOIDs { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<EventTypes> EventTypes { get; set; }
        public virtual DbSet<FacilityFIDs> FacilityFIDs { get; set; }
        public virtual DbSet<Industries> Industries { get; set; }
        public virtual DbSet<IndustrySectors> IndustrySectors { get; set; }
        public virtual DbSet<Machines> Machines { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<Traders> Traders { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admUsers>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<admUsers>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<admUsers>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<admUsers>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<admUsersRoles>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<DataTypes>()
                .HasMany(e => e.DataTypes1)
                .WithOptional(e => e.DataTypes2)
                .HasForeignKey(e => e.BaseDataTypeId);

            modelBuilder.Entity<Entities>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Entities)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityFields>()
                .Property(e => e.Cardinality)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C42Order>()
                .HasMany(e => e.C42Order_aUIs)
                .WithRequired(e => e.C42Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<C42Order>()
                .HasMany(e => e.C42Order_upUIs)
                .WithRequired(e => e.C42Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EconomicOperators>()
                .HasMany(e => e.EOFacilities)
                .WithRequired(e => e.EconomicOperators)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EconomicOperators>()
                .HasMany(e => e.EOIDs)
                .WithRequired(e => e.EconomicOperators)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EconomicOperators>()
                .HasMany(e => e.Traders)
                .WithOptional(e => e.EconomicOperators)
                .HasForeignKey(e => e.Seller);

            modelBuilder.Entity<EconomicOperators>()
                .HasMany(e => e.Traders1)
                .WithOptional(e => e.EconomicOperators1)
                .HasForeignKey(e => e.Buyer);

            modelBuilder.Entity<EconomicOperators>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.EconomicOperators)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EOFacilities>()
                .HasMany(e => e.FacilityFIDs)
                .WithRequired(e => e.EOFacilities)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EOFacilities>()
                .HasMany(e => e.Machines)
                .WithRequired(e => e.EOFacilities)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Events>()
                .HasMany(e => e.Events1)
                .WithOptional(e => e.Events2)
                .HasForeignKey(e => e.RelatedWith);

            modelBuilder.Entity<Events>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Events)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventTypes>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Industries>()
                .HasMany(e => e.IndustrySectors)
                .WithRequired(e => e.Industries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Industries>()
                .HasMany(e => e.Processes)
                .WithRequired(e => e.Industries)
                .WillCascadeOnDelete(false);
        }
    }
}