
using DAL.Models;

namespace DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext(string connectionString)
            : base(connectionString)
        {

        }
        public SocialNetworkContext() : base() { }
        static SocialNetworkContext()
        {
            Database.SetInitializer(new SocialNetworkInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Message>()
            .HasRequired(s => s.Chat)
            .WithMany(g => g.Messages)
            .HasForeignKey(s => s.ChatId)
            .WillCascadeOnDelete();


            modelBuilder.Entity<Message>()
                .HasOptional(s => s.Content)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);

         
            modelBuilder.Entity<FriendRequest>()
                .HasOptional(s => s.RequestedTo)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetails>().HasMany(m => m.Friends).WithMany();

            modelBuilder.Entity<UserDetails>().Property(d => d.DateOfBirth).HasColumnType("datetime2");

        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<UserDetails> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<FriendRequest> Friends { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }


    }



}