using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Data.Models;

namespace TwitterApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }
        #endregion
    }
}
