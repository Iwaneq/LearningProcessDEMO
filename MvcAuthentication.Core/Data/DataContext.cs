using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.ManyToMany;
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
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* --- LEVEL QUESTIONS --- */

            modelBuilder.Entity<LevelQuestion>()
                .HasOne(l => l.LevelProgressState)
                .WithMany(q => q.LevelQuestions)
                .HasForeignKey(l => l.LevelId);

            modelBuilder.Entity<LevelQuestion>()
                .HasOne(l => l.Question)
                .WithMany(q => q.LevelQuestions)
                .HasForeignKey(l => l.QuestionId);

            /* --- ANSWERED QUESTIONS --- */

            modelBuilder.Entity<AnsweredQuestion>()
                .HasOne(l => l.LevelProgressState)
                .WithMany(q => q.AnsweredQuestions)
                .HasForeignKey(l => l.LevelId);

            modelBuilder.Entity<AnsweredQuestion>()
                .HasOne(l => l.Question)
                .WithMany(q => q.AnsweredQuestions)
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
