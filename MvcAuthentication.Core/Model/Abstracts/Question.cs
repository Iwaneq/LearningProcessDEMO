using MvcAuthentication.Core.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Model.Abstracts
{
    public enum QuestionType
    {
        OneAnswer
    }
    public abstract class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public Answer GoodAnswer { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
