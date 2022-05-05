using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.ManyToMany;
using MvcAuthentication.Core.Model;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;
using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LevelProgressState> LevelProgressStates { get; set; }
        public DbSet<LevelQuestion> LevelQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UnansweredQuestion> UnansweredQuestion { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* --- LEVEL QUESTIONS --- */

            modelBuilder.Entity<LevelQuestion>()
                .HasOne(l => l.Level)
                .WithMany(q => q.LevelQuestions)
                .HasForeignKey(l => l.LevelId);

            modelBuilder.Entity<LevelQuestion>()
                .HasOne(l => l.Question)
                .WithMany(q => q.LevelQuestions)
                .HasForeignKey(l => l.QuestionId);

            /* --- ANSWERED QUESTIONS --- */

            modelBuilder.Entity<UnansweredQuestion>()
                .HasOne(l => l.LevelProgressState)
                .WithMany(q => q.UnansweredQuestions)
                .HasForeignKey(l => l.LevelId);

            modelBuilder.Entity<UnansweredQuestion>()
                .HasOne(l => l.Question)
                .WithMany(q => q.UnansweredQuestions)
                .HasForeignKey(l => l.QuestionId);



            /* --- QUESTION ANSWER --- */

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(x => x.Question)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.QuestionId);

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(x => x.Answer)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.AnswerId);

        }
    }
}
