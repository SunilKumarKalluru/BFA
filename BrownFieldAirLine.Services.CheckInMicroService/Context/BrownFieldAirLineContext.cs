using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Context
{
    ///<summary>
    ///This context class is used for communication between the database
    ///This class has models in form of Dbsets ie replication of tables from database
    ///<summary/>
    public class BrownFieldAirLineContext : DbContext
    {
        public BrownFieldAirLineContext(DbContextOptions<BrownFieldAirLineContext> options) : base(options)
        {
            
        }

        public DbSet<Baggage> baggages {get;set;}
        public DbSet<Boarding> boardings {get;set;}
        public DbSet<Booking> bookings {get;set;}
        public DbSet<BaggageWeightClass> baggageWeightClasses {get;set;}
        public DbSet<CheckIn> checkIns {get;set;}
        public DbSet<Loyalty> loyalties {get;set;}
        public DbSet<Passenger> passengers {get;set;}
        public DbSet<Seating> seatings {get;set;}
        public DbSet<User> users {get;set;}
        ///<summary>
        ///This Function has relation ships and foriegn keys configured in it.
        ///Helps Application during execution for proper relationships of objects
        ///<summary/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasMany<Passenger>(x => x.passengers)
                .WithOne(x => x.booking)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<Booking>()
                .HasMany<Seating>(x => x.seating)
                .WithOne(x => x.booking)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<Booking>()
                .HasMany<Boarding>(x => x.boarding)
                .WithOne(x => x.booking)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<Booking>()
                .HasMany<Baggage>(x => x.baggage)
                .WithOne(x => x.booking)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<Passenger>()
                .HasOne<Boarding>(x=>x.boarding)
                .WithOne(x=>x.passenger)
                .HasForeignKey<Boarding>(x=>x.PassengerId);
                
            modelBuilder.Entity<Booking>()
                .HasOne<CheckIn>(x => x.checkIn)
                .WithOne(x => x.booking)
                .HasForeignKey<CheckIn>(x => x.BookingId);
            
            modelBuilder.Entity<User>()
                .HasOne<Loyalty>(x =>x.loyalty)
                .WithOne(x => x.user)
                .HasForeignKey<Loyalty>(x => x.UserId);

            modelBuilder.Entity<Passenger>()
                .HasOne<Seating>(x => x.seating)
                .WithOne(x=>x.passenger)
                .HasForeignKey<Seating>(x =>x.PassengerId);

            modelBuilder.Entity<Passenger>()
                .HasOne<Baggage>(x => x.baggage)
                .WithOne(x => x.passenger)
                .HasForeignKey<Baggage>(x => x.PassengerId);
        }
    }
}