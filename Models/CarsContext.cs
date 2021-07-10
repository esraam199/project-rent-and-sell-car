using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class CarsContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Brand> Brand { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }
        public DbSet<CarPhoto> CarPhoto { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Dimension> Dimension { get; set; }
        public DbSet<Exterior> Exterior { get; set; }
        public DbSet<Interior> Interior { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelClass> ModelClass { get; set; }
        public DbSet<Multimedia> Multimedia { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<Safety> Safety { get; set; }
        public DbSet<SellingData> SellingData { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<UserCar> UserCar { get; set; }
        public DbSet<UserPhone> UserPhone { get; set; }


        public CarsContext(DbContextOptions options) : base(options)
        {}




    }
}
