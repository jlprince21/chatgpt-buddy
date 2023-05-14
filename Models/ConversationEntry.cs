using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
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
}