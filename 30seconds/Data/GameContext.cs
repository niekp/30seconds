using System;
using _30seconds.Models;
using Microsoft.EntityFrameworkCore;

namespace _30seconds.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Wordlist> Wordlist { get; set; }
        public DbSet<Word> Word { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=30seconds.db");
    }
}
