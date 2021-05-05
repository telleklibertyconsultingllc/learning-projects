using EX.EFC.Models;
using System;
using System.Collections.Generic;

namespace EX.EFC.DTOs
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public BetterName BetterName { get; set; }
    }
}
