﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieTime.DataAccessLibrary;

namespace MovieTime.DataAccessLibrary.Migrations
{
    [DbContext(typeof(MovieContext))]
    partial class MovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdMovie")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReviewScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMovie");

                    b.HasIndex("IdUser");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Popularity")
                        .HasColumnType("float");

                    b.Property<string>("PosterPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReviewScoreValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.MovieGenre", b =>
                {
                    b.Property<Guid>("IdMovie")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdGenre")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdMovie", "IdGenre");

                    b.HasIndex("IdGenre");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.MovieRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdMovie")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOf1ReviewStars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("NumberOf2ReviewStars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("NumberOf3ReviewStars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("NumberOf4ReviewStars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("NumberOf5ReviewStars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("IdMovie")
                        .IsUnique();

                    b.ToTable("MovieRating");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.UserMovieActivity", b =>
                {
                    b.Property<Guid>("IdMovie")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMovie", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("UserMovieActivity");
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.Comment", b =>
                {
                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.MovieGenre", b =>
                {
                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.MovieRating", b =>
                {
                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.Movie", "Movie")
                        .WithOne("MovieRating")
                        .HasForeignKey("MovieTime.ApplicationLogicLibrary.Models.MovieRating", "IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieTime.ApplicationLogicLibrary.Models.UserMovieActivity", b =>
                {
                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.Movie", "Movie")
                        .WithMany("RelatedListUsersActivity")
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieTime.ApplicationLogicLibrary.Models.User", "User")
                        .WithMany("RelatedListMovies")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
