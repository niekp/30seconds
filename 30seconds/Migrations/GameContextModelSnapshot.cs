﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _30seconds.Data;

namespace _30seconds.Migrations {
	[DbContext(typeof(GameContext))]
	partial class GameContextModelSnapshot : ModelSnapshot {
		protected override void BuildModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "5.0.2");

			modelBuilder.Entity("_30seconds.Models.Game", b => {
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("INTEGER");

				b.Property<int>("IdRoom")
					.HasColumnType("INTEGER");

				b.Property<DateTime>("Start")
					.HasColumnType("TEXT");

				b.Property<string>("User")
					.HasColumnType("TEXT");

				b.HasKey("Id");

				b.HasIndex("IdRoom");

				b.ToTable("Game");
			});

			modelBuilder.Entity("_30seconds.Models.Room", b => {
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("INTEGER");

				b.Property<int>("AmountOfSeconds")
					.HasColumnType("INTEGER");

				b.Property<DateTime>("Created")
					.HasColumnType("TEXT");

				b.Property<int>("IdWordlist")
					.HasColumnType("INTEGER");

				b.Property<DateTime>("LastPing")
					.HasColumnType("TEXT");

				b.Property<string>("Name")
					.HasColumnType("TEXT");

				b.HasKey("Id");

				b.HasIndex("IdWordlist");

				b.ToTable("Room");
			});

			modelBuilder.Entity("_30seconds.Models.Word", b => {
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("INTEGER");

				b.Property<int?>("GameId")
					.HasColumnType("INTEGER");

				b.Property<int>("IdWordlist")
					.HasColumnType("INTEGER");

				b.Property<string>("Text")
					.HasColumnType("TEXT");

				b.HasKey("Id");

				b.HasIndex("GameId");

				b.HasIndex("IdWordlist");

				b.ToTable("Word");
			});

			modelBuilder.Entity("_30seconds.Models.Wordlist", b => {
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("INTEGER");

				b.Property<string>("Title")
					.HasColumnType("TEXT");

				b.HasKey("Id");

				b.ToTable("Wordlist");
			});

			modelBuilder.Entity("_30seconds.Models.Game", b => {
				b.HasOne("_30seconds.Models.Room", "Room")
					.WithMany("Games")
					.HasForeignKey("IdRoom")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Room");
			});

			modelBuilder.Entity("_30seconds.Models.Room", b => {
				b.HasOne("_30seconds.Models.Wordlist", "Wordlist")
					.WithMany()
					.HasForeignKey("IdWordlist")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Wordlist");
			});

			modelBuilder.Entity("_30seconds.Models.Word", b => {
				b.HasOne("_30seconds.Models.Game", null)
					.WithMany("Words")
					.HasForeignKey("GameId");

				b.HasOne("_30seconds.Models.Wordlist", "Wordlist")
					.WithMany("Words")
					.HasForeignKey("IdWordlist")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Wordlist");
			});

			modelBuilder.Entity("_30seconds.Models.Game", b => {
				b.Navigation("Words");
			});

			modelBuilder.Entity("_30seconds.Models.Room", b => {
				b.Navigation("Games");
			});

			modelBuilder.Entity("_30seconds.Models.Wordlist", b => {
				b.Navigation("Words");
			});
#pragma warning restore 612, 618
		}
	}
}
