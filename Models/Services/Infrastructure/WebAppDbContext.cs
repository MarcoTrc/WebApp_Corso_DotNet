using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using webApp.Models.Entities;

namespace webApp.Models.Services.Infrastructure
{
    public partial class WebAppDbContext : DbContext
    {
        public WebAppDbContext()
        {
        }

        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Data/WebApp.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Course>(entity =>
            {
                //CODICE DI MAPPING:

                //Indichiamo su che tabella rimappa la nostra classe di entità
                entity.ToTable("Courses"); //Superfluo se la tabella si chiama come la proprietà che espone il Dbset

                //Indichiamo la proprietà all'interno della nostra classe che rappresenta la chiave primaria
                entity.HasKey(course => course.Id); //Superfluo se la proprietà si chiama Id oppure CourseId

                // Nel caso di chiavi primarie cmposite
                //entity.HasKey(course => new {course.Id, course.Author});

                // Nel caso di owned types (classi che aiutano a tenere coesi più valori) che non sono entità utilizziamo entity.OwnsOne(...) per il mapping
                entity.OwnsOne(course => course.FullPrice, builder =>
                {
                    builder.Property(money => money.Amount).HasColumnName("FullPrice_Amount");
                    builder.Property(money => money.Currency).HasConversion<string>().HasColumnName("FullPrice_Currency");
                });

                entity.OwnsOne(course => course.CurrentPrice, builder =>
                {
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount");
                    builder.Property(money => money.Currency).HasConversion<string>().HasColumnName("Currentprice_Currency");
                });

                // Mappare relazioni
                entity.HasMany(course => course.Lessons)
                .WithOne(lesson => lesson.Course)
                .HasForeignKey(lesson => lesson.CourseId); //superflua se la proprietà si chiama CourseId



                #region Mapping generato con reverse engineering
                /*
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.CurrentPriceAmount)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CurrentPriceCurrency)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                entity.Property(e => e.FullPriceAmount)
                    .IsRequired()
                    .HasColumnName("FullPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FullPriceCurrency)
                    .IsRequired()
                    .HasColumnName("FullPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");
                    */
                #endregion
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                #region Mapping generato con reverse engineering
                /*
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnType("TEXT (8)")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CourseId);
                    */
                #endregion
            });
        }
    }
}
