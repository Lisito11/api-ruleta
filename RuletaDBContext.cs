using System;
using ApiRuleta.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiRuleta
{
    public class RuletaDBContext : DbContext
    {
        public RuletaDBContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User>? Users { get; set; }
       
    }
}

