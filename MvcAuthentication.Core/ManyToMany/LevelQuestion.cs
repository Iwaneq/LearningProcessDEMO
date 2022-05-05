using MvcAuthentication.Core.Model;
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
    public class LevelQuestion
    {
        [Key]
        public int Id { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
