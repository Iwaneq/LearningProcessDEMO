using MvcAuthentication.Core.ManyToMany;
using MvcAuthentication.Core.Model;
using MvcAuthentication.Core.Model.Abstracts;
using MvcAuthentication.Core.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.State
{
    public class LevelProgressState
    {
        [Key]
        public int Id { get; set; }

        public Level Level { get; set; }

        public List<UnansweredQuestion> UnansweredQuestions { get; set; }

        [Range(0, 100)]
        public float ProgressPrecentage { get; set; }

        [Required]
        public bool IsFinished { get; set; } = false;

        [Required]
        public Account Account { get; set; }
    }
}
