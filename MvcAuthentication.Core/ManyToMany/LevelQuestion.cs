using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.ManyToMany
{
    public class LevelQuestion
    {
        public LevelProgressState LevelProgressState { get; set; }
        public int LevelId { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
