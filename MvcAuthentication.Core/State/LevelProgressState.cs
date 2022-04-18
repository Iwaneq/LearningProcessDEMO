using MvcAuthentication.Core.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.State
{
    public abstract class LevelProgressState
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public List<Question> TotalQuestions { get; set; }
        public List<Question> AnsweredQuestions { get; set; }
        public List<Question> LeftQuestions { get; set; }
        public float ProgressPrecentage { get; set; }

        public LevelProgressState()
        {
            TotalQuestions = new List<Question>();
            AnsweredQuestions = new List<Question>();
            LeftQuestions = new List<Question>();
        }
    }
}
