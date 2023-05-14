using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
    public class AppliedTag
    {
        [Key]
        public string Id { get; set;}

        public string ChatId { get; set;} = default!;
        public string TagId { get; set;} = default!;
    }
}