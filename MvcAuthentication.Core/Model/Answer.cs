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
    [BindProperties]
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string AnswerText { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
