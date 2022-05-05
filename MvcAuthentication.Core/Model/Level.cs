using MvcAuthentication.Core.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Model
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string LevelName { get; set; }

        public List<LevelQuestion> LevelQuestions { get; set; }
    }
}
