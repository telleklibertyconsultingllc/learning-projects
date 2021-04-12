using EX.EFC.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EX.EFC.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}
