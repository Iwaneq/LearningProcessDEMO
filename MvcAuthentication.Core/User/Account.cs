using MvcAuthentication.Core.State;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.User
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string PasswordHash { get; set; }

        [MaxLength(40)]
        public string RefreshToken { get; set; }

        public List<LevelProgressState> LevelsProgress { get; set; }
    }
}
