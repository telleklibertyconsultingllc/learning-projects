using EX.EFC.DTOs;
using EX.EFC.Models;
using System;
using System.Linq;

namespace EX.EFC.ConsoleApp
{
    class Program
    {
        private static readonly SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            Console.WriteLine(args);
            _context.Database.EnsureCreated();
            GetSamurais("Before Add:");
            AddSamurai();
            GetSamurais("After Add:");
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void GetSamurais(string label)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{label}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai
            {
                Name = "John Sampson"
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}
