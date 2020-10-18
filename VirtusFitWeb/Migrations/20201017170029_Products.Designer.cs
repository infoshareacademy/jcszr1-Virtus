﻿// <auto-generated />
using BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace VirtusFitWeb.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20201017170029_Products")]
    partial class Products
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("BLL.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("float");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<int>("Fiber")
                        .HasColumnType("int");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<int>("PortionQuantity")
                        .HasColumnType("int");

                    b.Property<string>("PortionUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Salt")
                        .HasColumnType("float");

                    b.Property<int>("Sugar")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
