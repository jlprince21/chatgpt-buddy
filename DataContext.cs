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
            // TODO 2023-03-21 add env var support
            // optionsBuilder.UseSqlite(Environment.GetEnvironmentVariable("CHATGPT_BUDDY_SQLITE_PATH"));
            optionsBuilder.UseSqlite("DataSource=/path/to/your/gpt_database.db");
        }
    }

    public class ConversationEntry
    {
        [Key]
        public int Id { get; set;}

        public string ChatId { get; set;} = default!;
        public int Sequence { get; set; }

        public Speaker Speaker { get; set;}
        public string? Text { get; set; }
        public DateTime EventTime { get; set; }
    }

    public class Tag
    {
        [Key]
        public string Id { get; set;}

        public string Text { get; set;}  = default!;
    }

    public class AppliedTag
    {
        [Key]
        public string Id { get; set;}

        public string ChatId { get; set;} = default!;
        public string TagId { get; set;} = default!;
    }

    public enum Speaker
    {
        Unknown = 0,
        Human = 1,
        ChatGPT = 2
    }
}