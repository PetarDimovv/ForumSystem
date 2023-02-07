using Microsoft.EntityFrameworkCore;
using StackOverflow.Models;
using StackOverflow.Models.Enum;
using System.Collections.Generic;

namespace StackOverflow.Data

{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Comment>()
                .HasOne(comment => comment.Post)
                .WithMany(posts => posts.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Comment>()
                .HasOne(comment => comment.CreatedBy)
                .WithMany(user => user.Comments)
                .OnDelete(DeleteBehavior.NoAction);




            List<User> users = new List<User>()
            {
                new User { Id = 1, /*FirstName = "Aleksandru", LastName = "Aleksandrov", Email = "aleksacho43@gmail.com",*/ Username = "Aleks77", /*Password = 12343,*/ IsAdmin = false },
                new User { Id = 2, /*FirstName = "Pesho", LastName = "Peshevski", Email = "paco99@abv.bg", */Username = "PeshoEQk", /*Password = 123333 ,*/ IsAdmin = false },
                new User { Id = 3, /*FirstName = "Piotur", LastName = "Pashev", Email = "piotru@yahoo.com",*/ Username = "Piotri", /*Password = 123434 ,*/ IsAdmin = true }
            };
            modelBuilder.Entity<User>().HasData(users);

            List<Post> posts = new List<Post>()
            {
                new Post { Id = 1, Title = "How to create a SQL server", Content = "Try to follow this guide *Link* ", UserId = 1 },
                new Post { Id = 2, Title = "How to delete a SQL Server", Content = "Try to follow this guide *link* ", UserId = 3 },
                new Post { Id = 3, Title = "How to update a SQL SErver", Content = "Try to follow this guide *LInk* ", UserId = 2 }
            };
            modelBuilder.Entity<Post>().HasData(posts);

            List<Comment> comments = new List<Comment>()
            {
                new Comment { Id = 1, Content = "I Don't see this link. Can you send me here.", UserId = 2, PostId = 1 },//TODO an idea - could create a seperate table with userID to connect with users
                new Comment { Id = 2, Content = "I don't See this link. Can you send me here.", UserId = 1, PostId = 2 },//And only add the phone number as an optional strin or int
                new Comment { Id = 3, Content = "I don'T see this link. Can you send me here.", UserId = 3, PostId = 3 }//String bc it could be added with +359, +358 etc for foreign countries 
            };
            modelBuilder.Entity<Comment>().HasData(comments);

            base.OnModelCreating(modelBuilder);

        }
    }
}
