using Microsoft.AspNetCore.Mvc;
using MvcAuthentication.Core.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Model.Abstracts
{
    public enum QuestionType
    {
        OneAnswer
    }

    [BindProperties]
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        [MaxLength(50)]
        public string QuestionText { get; set; } = string.Empty;

        public Answer GoodAnswer { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public List<LevelQuestion> LevelQuestions { get; set; }
        public List<UnansweredQuestion> UnansweredQuestions { get; set; }
    }
}
