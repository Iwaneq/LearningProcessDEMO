using MvcAuthentication.Core.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Model.Abstracts
{
    public abstract class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
