using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
    public class DataContext : DbContext
    {
        public DbSet<AppliedTag> AppliedTags { get; set; }
        public DbSet<ConversationEntry> Conversations { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Environment.GetEnvironmentVariable("CGB_DB_CONNECTION"));
        }
    }

    // public enum Speaker
    // {
    //     Unknown = 0,
    //     Human = 1,
    //     ChatGPT = 2
    // }
}