﻿using DataAccess.EShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.EntitiesFramework
{
    public class EShopDBContext : DbContext
    {
        public EShopDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Function> Function { get; set; }
        public virtual DbSet<UserFunction> UserFunction { get; set; }
    }
    
}
