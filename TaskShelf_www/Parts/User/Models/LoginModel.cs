using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskShelf_www.Parts.User.Models
{

    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]{6,}")]
        public string Password { get; set; }
    }
}