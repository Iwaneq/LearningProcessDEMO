using Microsoft.EntityFrameworkCore;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.ManyToMany
{
    public class AnsweredQuestion
    {
        [Key]
        public int Id { get; set; }
        public int LevelId { get; set; }
        public LevelProgressState LevelProgressState { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
