using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
    public enum Speaker
    {
        Unknown = 0,
        Human = 1,
        ChatGPT = 2
    }
}