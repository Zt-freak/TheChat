using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Entities;

namespace TheChat.Business
{
    public class ChatDbContext : IdentityDbContext<User>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public ChatDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TheChat;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.IsBanned)
                .HasDefaultValue(false);
        }
    }
}
