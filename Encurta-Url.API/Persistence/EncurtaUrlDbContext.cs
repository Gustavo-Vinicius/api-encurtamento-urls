using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encurta_Url.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Encurta_Url.API.Persistence
{
    public class EncurtaUrlDbContext : DbContext
    {


        public EncurtaUrlDbContext(DbContextOptions<EncurtaUrlDbContext> options)
        : base(options)
        {

        }
        public List<ShortenedCustomLink> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShortenedCustomLink>(e => {
                e.HasKey(l => l.Id);
            });
        }

    }
}